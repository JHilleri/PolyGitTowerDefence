using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Soldat : MonoBehaviour, Pausable{

    public int camp; // 1 = gauche 2 = droite
    public int chemin; // numero du chemin suivi par ce Soldat
    public int argentGagne;//Argent donné à l'adversaire lors de la destruction de cette unité
    public int vieRetireeAuJoueur;//La vie qui est retiré au joueur adverse lorsque l'unité passe sa défense
    public int xpGeneree;//Quantité de expérience générée par l'unité lors de sa destruction (Pour l'adversaire)
    public float vieMax; // Vie max du soldat
    public int tempsRecharge; // nombre de frames entre chaque attaque
    public float degats; // degat causé par chaque attaque
    public float portee; // portee des attaques
    public float detect; // portee de detection
    public float vitesse; // vitesse de déplacement du soldat
    public float marge; // distance à laquelle il considère avoir atteint un objectif
    public bool lanceProjectiles; // ce soldat lance-t-il des projectiles ?
    public GameObject projectile; // projectile que ce soldat lance (null si lanceProjectiles = false)
    public Sprite imageFace; // image non colorisée de face
    public Sprite imageFaceCouleur; //image colorisée par l'élément de face
    public Sprite imageDos;
    public Sprite imageDosCouleur;
    public Sprite imageGauche;
    public Sprite imageGaucheCouleur;
    public Sprite imageDroite;
    public Sprite imageDroiteCouleur;
    public Element element; //élément du soldat
    private float vie;
    private int etape;
    private int cooldown;
    private PointPassage objectif;
    private Soldat cible;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer colorSpriteRenderer;
    private float oldDistance;
    private float distanceX;
    private float distanceY;
    private float distance;
    private float vitesseX;
    private float vitesseY;
    private bool paused;
    private bool enCombat;

	// Use this for initialization
	void Start () {
        if (camp == 1)// Si ce soldat est dans le camps de gauche, sa première étape est la numéro 0
        {
            etape = 0;
        }
        else // Sinon il commence à la derniere étape
        {
            etape = pointMax(chemin);
        }
        objectif = null;
        cible = null;
        vie = vieMax;
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
        cooldown = 0;
        enCombat = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)// Vérifie que le soldat n'est pas en pause
        {
            if (!enCombat)// Si le soldat n'est pas en combat
            {
                Soldat[] listeSoldats = FindObjectsOfType<Soldat>();
                float minDist = -1;
                float dist = 0;
                Soldat pCible = null;
                foreach(Soldat sol in listeSoldats)
                {
                    dist = calcDistance(sol.gameObject);
                    if (sol.camp != camp && (minDist == -1 || dist < minDist))
                    {
                        minDist = dist;
                        pCible = sol;
                    }
                }
                if (pCible != null && (minDist < detect))
                {
                    cible = pCible;
                    objectif = null;
                    enCombat = true;
                }
                if (objectif == null)// s'il n'a pas d'objectif en cours
                {
                    PointPassage[] points = UnityEngine.Object.FindObjectsOfType<PointPassage>();
                    bool found = false;
                    int indexPoints = 0;
                    while (indexPoints < points.Length && !found)
                    {
                        if (points[indexPoints].numeroChemin == chemin && points[indexPoints].ordre == etape)
                        {
                            objectif = points[indexPoints];
                            distanceX = objectif.transform.position.x - transform.position.x;
                            distanceY = objectif.transform.position.y - transform.position.y;
                            distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
                            oldDistance = distance;
                            vitesseX = (distanceX / distance) * vitesse;
                            vitesseY = (distanceY / distance) * vitesse;
                            found = true;
                        }
                        indexPoints++;
                    }
                    if (!found)
                    {
                        gagne();
                    }
                }
                if (transform != null && objectif != null)
                {
                    distanceX = objectif.transform.position.x - transform.position.x;
                    distanceY = objectif.transform.position.y - transform.position.y;
                    distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
                    if (distance < marge || distance > oldDistance)
                    {
                        if (camp == 1)
                        {
                            etape++;
                        }
                        else
                        {
                            etape--;
                        }
                        objectif = null;
                    }
                    else
                    {
                        transform.Translate(vitesseX, vitesseY, 0);
                    }
                    oldDistance = distance;
                }
            }
            else // Si le soldat est actuellement en combat
            {
                if (cible != null)
                {
                    distanceX = cible.transform.position.x - transform.position.x;
                    distanceY = cible.transform.position.y - transform.position.y;
                    distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
                    if (distance < portee)
                    {
                        if (cooldown <= 0)
                        {
                            attaque(cible);
                        }
                    }
                    else
                    {
                        vitesseX = (distanceX / distance) * vitesse;
                        vitesseY = (distanceY / distance) * vitesse;
                        transform.Translate(vitesseX, vitesseY, 0);
                    }
                }
                else
                {
                    enCombat = false;
                }
            }
            if (Mathf.Abs(vitesseX) > Mathf.Abs(vitesseY)) // Les lignes qui suivent changent l'image en fonction de la direction du soldat
            {
                if (vitesseX > 0)
                {
                    spriteRenderer.sprite = imageDroite;
                    colorSpriteRenderer.sprite = imageDroiteCouleur;
                }
                else
                {
                    spriteRenderer.sprite = imageGauche;
                    colorSpriteRenderer.sprite = imageGaucheCouleur;
                }
            }
            else
            {
                if (vitesseY > 0)
                {
                    spriteRenderer.sprite = imageDos;
                    colorSpriteRenderer.sprite = imageDosCouleur;
                }
                else
                {
                    spriteRenderer.sprite = imageFace;
                    colorSpriteRenderer.sprite = imageFaceCouleur;
                }
            }
            if (vie <= 0)
            {
                meurt();
            }
            if(cooldown > 0)
            {
                cooldown--;
            }
        }
    }

    float calcDistance(GameObject autre)
    {
        float x = autre.transform.position.x - transform.position.x;
        float y = autre.transform.position.y - transform.position.y;
        return Mathf.Sqrt(x * x + y * y);
    }

    int pointMax (int chemin)
    {
        PointPassage[] points = UnityEngine.Object.FindObjectsOfType<PointPassage>();
        int max = 0;
        foreach(PointPassage point in points)
        {
            if (point.numeroChemin == chemin && point.ordre > max)
            {
                max = point.ordre;
            }
        }
        return max;
    }

    public void degat(float dgt)
    {
        vie -= dgt;
    }

    void attaque(Soldat ennemi)
    {
        ennemi.degat(ennemi.element.lireRatioDegat(element) * degats);
        cooldown = tempsRecharge;
    }

    void gagne()
    {
        if (camp == 1)
        {
            GameObject.FindObjectOfType<Partie>().joueurDroit.vie -= vieRetireeAuJoueur;
            GameObject.FindObjectOfType<Partie>().joueurGauche.argent += argentGagne;
            GameObject.FindObjectOfType<Partie>().joueurGauche.experience += xpGeneree;
        } else
        {
            GameObject.FindObjectOfType<Partie>().joueurGauche.vie -= vieRetireeAuJoueur;
            GameObject.FindObjectOfType<Partie>().joueurDroit.argent += argentGagne;
            GameObject.FindObjectOfType<Partie>().joueurDroit.experience += xpGeneree;
        }
        Destroy(gameObject);
    }

    void meurt()
    {
        Destroy(gameObject);
        if (camp == 1)
        {
            GameObject.FindObjectOfType<Partie>().joueurDroit.argent += argentGagne;
            GameObject.FindObjectOfType<Partie>().joueurDroit.experience += xpGeneree;
        }
        else
        {
            GameObject.FindObjectOfType<Partie>().joueurGauche.argent += argentGagne;
            GameObject.FindObjectOfType<Partie>().joueurDroit.experience += xpGeneree;
        }
    }

    public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
}
