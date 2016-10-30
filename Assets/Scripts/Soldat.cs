using UnityEngine;
using System.Collections.Generic;
using System;

public class Soldat : Unite{
    public int chemin; // numero du chemin suivi par ce Soldat
    public int etape;
    public int argentGagne;//Argent donné à l'adversaire lors de la destruction de cette unité
    public int vieRetireeAuJoueur;//La vie qui est retirée au joueur adverse lorsque l'unité passe sa défense
    public int xpGeneree;//Quantité d'expérience générée par l'unité lors de sa destruction (Pour l'adversaire)
    public int tempsRecharge; // nombre de frames entre chaque attaque
    public float portee; // portee des attaques
    public float detect; // portee de detection
    public float marge; // distance à laquelle il considère avoir atteint un objectif
    public bool lanceProjectiles; // ce soldat lance-t-il des projectiles ?
    public GameObject projectile; // projectile que ce soldat lance (null si lanceProjectiles = false)
    public Sprite imageFace; // image non colorisée de face. l'image est definie suivant la classe du soldat
    public Sprite imageFaceCouleur; //image colorisée par l'élément de face
    public Sprite imageDos;
    public Sprite imageDosCouleur;
    public Sprite imageGauche;
    public Sprite imageGaucheCouleur;
    public Sprite imageDroite;
    public Sprite imageDroiteCouleur;
    public bool monte;
    public bool occupe;
    public bool paralise;
    public UniteTour monture;
    public AudioClip sonMort;
    public AudioClip sonCombat;
    private int cooldown;
    private PointPassage objectif;
    private Unite cible;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer colorSpriteRenderer;
    private float oldDistance;
    private float distanceX;
    private float distanceY;
    private float distance;
    private float vitesseX;
    private float vitesseY;
    private bool enCombat;


    // Use this for initialization
    override protected void Start() {
        base.Start();

        objectif = null;
        cible = null;
        hitPoints = maxHitPoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
        cooldown = 0;
        enCombat = false;
    }
	
	// Update is called once per frame
	override protected void FixedUpdate() {
        base.FixedUpdate();
        if (!Pause.isPaused)
        {
            if (!paralise && !isStun)// Vérifie que le soldat n'est pas en pause
            {
                if (!enCombat)// Si le soldat n'est pas en combat
                {
                    Soldat[] listeSoldats = FindObjectsOfType<Soldat>();
                    float minDist = detect + 1; // on initialise la distance minimale quand étant supérieur à sa portée de détection
                    float dist;
                    Soldat pCible = null;
                    foreach (Soldat sol in listeSoldats)
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
                                vitesseX = (distanceX / distance) * effectiveSpeed;
                                vitesseY = (distanceY / distance) * effectiveSpeed;
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
                            vitesseX = (distanceX / distance) * effectiveSpeed;
                            vitesseY = (distanceY / distance) * effectiveSpeed;
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
                if (cooldown > 0)
                {
                    cooldown--;
                }
            }
            if (hitPoints <= 0)
            {
                meurt();
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

    public void recalculeTrajectoireVitesse()
    {
        objectif = null;
    }

    void attaque(Unite ennemi)
    {
        ennemi.degat(ennemi.element.lireRatioDegat(element) * effectivesDamages);
        if (sonCombat != null)
        {
            AudioSource.PlayClipAtPoint(sonCombat, Vector3.one, 1);
        }
        cooldown = tempsRecharge;
    }

    void gagne()
    {
        if (monture != null)
        {
            monture.meurt();
        }
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
        if (monture != null)
        {
            monture.meurt();
        }
        Destroy(gameObject);
        if (sonMort != null)
        {
            AudioSource.PlayClipAtPoint(sonMort, Vector3.one, 1);
        }
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

    public float getVie()
    {
        return hitPoints;
    }

    public int getDirection()
    {
        if (Mathf.Abs(vitesseX) > Mathf.Abs(vitesseY))
        {
            if (vitesseX > 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            if (vitesseY > 0)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }


    public void setCible(Unite nCible)
    {
        cible = nCible;
        objectif = null;
        enCombat = true;
    }

    
}
