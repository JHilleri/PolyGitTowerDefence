using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;

public class Soldat : MonoBehaviour, Pausable{

    public int camp; // 1 = gauche 2 = droite
    public int chemin; // numero du chemin suivi par ce Soldat
    public int argentGagne;//Argent donné à l'adversaire lors de la destruction de cette unité
    public int vieRetireeAuJoueur;//La vie qui est retirée au joueur adverse lorsque l'unité passe sa défense
    public int xpGeneree;//Quantité d'expérience générée par l'unité lors de sa destruction (Pour l'adversaire)
    public float vieMax; // Vie max du soldat
    public int tempsRecharge; // nombre de frames entre chaque attaque
    public float degats; // degat causé par chaque attaque
    public float portee; // portee des attaques
    public float detect; // portee de detection
    public float normalSpeed;
    public float vitesse; // vitesse de déplacement du soldat
    public float marge; // distance à laquelle il considère avoir atteint un objectif
    public bool lanceProjectiles; // ce soldat lance-t-il des projectiles ?
    public GameObject projectile; // projectile que ce soldat lance (null si lanceProjectiles = false)
    public int classe; //la classe du soldat : archer = 0 - brute = 1 - magicien = 2 - paysan = 3 - soldat = 4 - tank = 5
    public Element element; //élément du soldat
    private float vie;
    private int etape;
    private int cooldown;
    private PointPassage objectif;
    private Soldat cible;
    private Sprite imageFace; // image non colorisée de face. l'image est definie suivant la classe du soldat
    private Sprite imageFaceCouleur; //image colorisée par l'élément de face
    private Sprite imageDos;
    private Sprite imageDosCouleur;
    private Sprite imageGauche;
    private Sprite imageGaucheCouleur;
    private Sprite imageDroite;
    private Sprite imageDroiteCouleur;
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

    private List<Effect> effects = new List<Effect>();

    // Use this for initialization
    void Start() {
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
        if (classe == 0)
        {
            imageFace = Resources.Load("Assets/archer_face.PNG", typeof(Sprite)) as Sprite;
            imageFaceCouleur = Resources.Load("Assets/archer_face - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDos = Resources.Load("Assets/archer_dos.PNG", typeof(Sprite)) as Sprite;
            imageDosCouleur = Resources.Load("Assets/archer_dos - couleur.PNG", typeof(Sprite)) as Sprite;
            imageGauche = Resources.Load("Assets/archer_gauche.PNG", typeof(Sprite)) as Sprite;
            imageGaucheCouleur = Resources.Load("Assets/archer_gauche - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDroite = Resources.Load("Assets/archer_droite.PNG", typeof(Sprite)) as Sprite;
            imageDroiteCouleur = Resources.Load("Assets/archer_doite - couleur.PNG", typeof(Sprite)) as Sprite;
        }
        else if (classe == 1)
        {
            imageFace = Resources.Load("Assets/brute_face.PNG", typeof(Sprite)) as Sprite;
            imageFaceCouleur = Resources.Load("Assets/brute_face - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDos = Resources.Load("Assets/brute_dos.PNG", typeof(Sprite)) as Sprite;
            imageDosCouleur = Resources.Load("Assets/brute_dos - couleur.PNG", typeof(Sprite)) as Sprite;
            imageGauche = Resources.Load("Assets/brute_gauche.PNG", typeof(Sprite)) as Sprite;
            imageGaucheCouleur = Resources.Load("Assets/brute_gauche - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDroite = Resources.Load("Assets/brute_droite.PNG", typeof(Sprite)) as Sprite;
            imageDroiteCouleur = Resources.Load("Assets/brute_doite - couleur.PNG", typeof(Sprite)) as Sprite;
        }
        else if (classe == 2)
        {
            imageFace = Resources.Load("Assets/magicien_face.PNG", typeof(Sprite)) as Sprite;
            imageFaceCouleur = Resources.Load("Assets/magicien_face - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDos = Resources.Load("Assets/magicien_dos.PNG", typeof(Sprite)) as Sprite;
            imageDosCouleur = Resources.Load("Assets/magicien_dos - couleur.PNG", typeof(Sprite)) as Sprite;
            imageGauche = Resources.Load("Assets/magicien_gauche.PNG", typeof(Sprite)) as Sprite;
            imageGaucheCouleur = Resources.Load("Assets/magicien_gauche - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDroite = Resources.Load("Assets/magicien_droite.PNG", typeof(Sprite)) as Sprite;
            imageDroiteCouleur = Resources.Load("Assets/magicien_doite - couleur.PNG", typeof(Sprite)) as Sprite;
        }
        else if (classe == 3)
        {
            imageFace = Resources.Load("Assets/paysan_face.PNG", typeof(Sprite)) as Sprite;
            imageFaceCouleur = Resources.Load("Assets/paysan_face - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDos = Resources.Load("Assets/paysan_dos.PNG", typeof(Sprite)) as Sprite;
            imageDosCouleur = Resources.Load("Assets/paysan_dos - couleur.PNG", typeof(Sprite)) as Sprite;
            imageGauche = Resources.Load("Assets/paysan_gauche.PNG", typeof(Sprite)) as Sprite;
            imageGaucheCouleur = Resources.Load("Assets/paysan_gauche - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDroite = Resources.Load("Assets/paysan_droite.PNG", typeof(Sprite)) as Sprite;
            imageDroiteCouleur = Resources.Load("Assets/paysan_doite - couleur.PNG", typeof(Sprite)) as Sprite;
        }
        else if (classe == 4)
        {
            imageFace = Resources.Load("Assets/soldat_de_base_face.PNG", typeof(Sprite)) as Sprite;
            imageFaceCouleur = Resources.Load("Assets/soldat_de_base_face - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDos = Resources.Load("Assets/soldat_de_base_dos.PNG", typeof(Sprite)) as Sprite;
            imageDosCouleur = Resources.Load("Assets/soldat_de_base_dos - couleur.PNG", typeof(Sprite)) as Sprite;
            imageGauche = Resources.Load("Assets/soldat_de_base_gauche.PNG", typeof(Sprite)) as Sprite;
            imageGaucheCouleur = Resources.Load("Assets/soldat_de_base_gauche - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDroite = Resources.Load("Assets/soldat_de_base_droite.PNG", typeof(Sprite)) as Sprite;
            imageDroiteCouleur = Resources.Load("Assets/soldat_de_base_doite - couleur.PNG", typeof(Sprite)) as Sprite;
        }
        else // if (classe == 5)
        {
            imageFace = Resources.Load("Assets/tank_face.PNG", typeof(Sprite)) as Sprite;
            imageFaceCouleur = Resources.Load("Assets/tank_face - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDos = Resources.Load("Assets/tank_dos.PNG", typeof(Sprite)) as Sprite;
            imageDosCouleur = Resources.Load("Assets/tank_dos - couleur.PNG", typeof(Sprite)) as Sprite;
            imageGauche = Resources.Load("Assets/tank_gauche.PNG", typeof(Sprite)) as Sprite;
            imageGaucheCouleur = Resources.Load("Assets/tank_gauche - couleur.PNG", typeof(Sprite)) as Sprite;
            imageDroite = Resources.Load("Assets/tank_droite.PNG", typeof(Sprite)) as Sprite;
            imageDroiteCouleur = Resources.Load("Assets/tank_doite - couleur.PNG", typeof(Sprite)) as Sprite;
        }
        cooldown = 0;
        enCombat = false;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (!paused)// Vérifie que le soldat n'est pas en pause
        {
            vitesse = normalSpeed;
            if(effects.Count != 0)
            {
                effects.RemoveAll(effect => (effect.haveDuration && effect.duration <= 0));
                foreach(Effect effect in effects)
                {
                    applyEffect(effect);
                    if (effect.haveDuration) effect.duration--;
                }
            }
            

            if (!enCombat)// Si le soldat n'est pas en combat
            {
                Soldat[] listeSoldats = FindObjectsOfType<Soldat>();
                float minDist = detect + 1; // on initialise la distance minimale quand étant supérieur à sa portée de détection
                float dist;
                Soldat pCible = null;
                foreach(Soldat sol in listeSoldats)
                {
                    dist = calcDistance(sol.gameObject);
                    if (sol.camp != camp && dist < minDist)
                    {
                        minDist = dist;
                        pCible = sol;
                    }
                }
                if (pCible != null)
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

    float calcDistance(GameObject other)
    {
        return ((Vector2)other.transform.position - (Vector2)transform.position).magnitude;
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

    void applyEffect(Effect effect)
    {
        vitesse *= effect.speedRelativeModifier;
        vitesse += effect.speedAbsoluteModifier;
    }

    public void addEffect(Effect effectToAdd)
    {
        Effect actualVersion = effects.Find( effect => (effect.name == effectToAdd.name));
        if (actualVersion == null) effects.Add(effectToAdd.Clone());
        else actualVersion = effectToAdd.Clone();
    }
}
