using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tour : MonoBehaviour{

    public GameObject cible;
    public GameObject projectileToFire;
    public int camp; // définit le camp auquel la tour appartient 1 -> gauche, 2 -> droite
    public int cout;// Cout de la tourelle
    public bool buf_allie;
    public bool tir_ennemi;
    public TargetType targetType = TargetType.enemy;
    public bool spawner;
    public bool projectile_obstacle;
    public bool effetZone;
    public ZoneEffet scriptEffetZone;
    public GameObject uniteSpawn;
    public int intervalleSpawn;
    public int unitesMax;
    public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    public float projectSpeed;
    public int intervalle;
    public List<EvolutionBatiment> ameliorations;
    public int range;
    internal SpriteRenderer colorSpriteRenderer;
    protected int compteur = 0;
    internal int compteurSpawn;
    internal int unitesEnVie;
    internal GameObject menu;
    internal bool menuActif;
    internal bool stopTirs;
    public AudioClip sonProjectile;
    private Joueur proprietaire;

    // Use this for initialization
    protected virtual void Start () {
        compteur = 0;
        compteurSpawn = 0;
        unitesEnVie = 0;
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
        //menu = GetComponentInChildren<MenuContextuelTour>().gameObject;
        //menu.SetActive(false);
        //menuActif = false;
        stopTirs = false;
        Joueur[] listeJoueurs = FindObjectsOfType<Joueur>();
        foreach (Joueur joueur in listeJoueurs)
        {
            if (joueur.camp == camp)
            {
                proprietaire = joueur;
            }
        }
    }
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        /*if (menuActif)
        {
            if (Input.GetMouseButtonDown(0))
            {
                menuActif = false;
            }
        }
        else if (menu != null)
        {
            menu.SetActive(false);
        }*/
        if (!Pause.isPaused)
        {
            if (compteur < intervalle)
            {
                compteur++;
            }
            else if ( targetType != TargetType.none || !stopTirs)
            {
                if(effetZone && scriptEffetZone != null)
                {
                    scriptEffetZone.origine = gameObject;
                    scriptEffetZone.camp = camp;
                    scriptEffetZone.active();
                }
                Soldat[] cibles = UnityEngine.Object.FindObjectsOfType<Soldat>();
                if (cibles.Length >= 1)
                {
                    float dist = 0;
                    float minDist = range + 1; // on initialise la la distance minimale de tir supérieur à la portée de la tour (un cas ou cela ne tire pas si aucune cible est à portée) 
                    foreach (Soldat pCible in cibles)
                    {
                        dist = distance(pCible.gameObject);
                        if (dist < minDist && (((targetType & TargetType.enemy) != 0 && pCible.camp != camp) || ((targetType & TargetType.ally) != 0 && pCible.camp == camp)))
                        {
                            cible = pCible.gameObject;
                            minDist = dist;
                        }
                    }
                    if (!projectile_obstacle) // si la tourelle envoi pas des obstacles, la coorddonnée cible est celle de l'étape du soldat
                    {
                        Projectile_old[] listeProjectiles = FindObjectsOfType<Projectile_old>();
                        foreach (Projectile_old proj in listeProjectiles)
                        {
                            if (proj.GetComponent<Projectile_old>().attaquable)
                            {
                                dist = distance(proj.gameObject);
                                if (proj.camp != camp && dist < minDist)
                                {
                                    minDist = dist;
                                    cible = proj.gameObject;
                                }
                            }
                        }
                    }
                    if (minDist > range)
                    {
                        cible = null;
                    }
                }
                compteur = 0;
                if (cible != null)
                {
                    tir();
                }
            }
            if (spawner)
            {
                if(compteurSpawn < intervalleSpawn)
                {
                    compteurSpawn++;
                }
                else if(unitesEnVie < unitesMax)
                {
                    GameObject spawned = Instantiate(uniteSpawn);
                    spawned.GetComponent<UniteTour>().tour = this;
                    spawned.GetComponent<UniteTour>().camp = camp;
                    spawned.transform.position = transform.position;
                    unitesEnVie++;
                    compteurSpawn = 0;
                }
            }
        }
	}
    /*
    void OnMouseDown()
    {
        menu.SetActive(true);
        menuActif = true;
    }*/

    void tir()
    {
        GameObject firedProjectile = Instantiate(projectileToFire);
        firedProjectile.GetComponent<SpriteRenderer>().color = element.couleur;
        firedProjectile.transform.position = transform.position;
        firedProjectile.transform.parent = transform;
        Projectile_old script = firedProjectile.GetComponent<Projectile_old>();
        script.element = element;
        script.camp = camp;
        if (projectile_obstacle)
        {
            bool pointTrouve = false;
            int indicePoint = 0;
            int chemin = cible.GetComponent<Soldat>().chemin;
            int etape = cible.GetComponent<Soldat>().etape; ;
            PointPassage[] points = FindObjectsOfType<PointPassage>();

            while (!pointTrouve)
            {
                if (points[indicePoint].GetComponent<PointPassage>().numeroChemin == chemin && points[indicePoint].GetComponent<PointPassage>().ordre == etape)
                {
                    script.target = points[indicePoint].transform.position;
                    pointTrouve = true;
                }
                indicePoint++;
            }
        }
        else
            script.target = cible.transform.position;
        script.speed = projectSpeed;
        script.portee = range * 2;
        script.tour = this;
        if (sonProjectile != null)
        {
            AudioSource.PlayClipAtPoint(sonProjectile, Vector3.one, 1);
        }

    }

    public void evolue (int numero)
    {
        if (proprietaire.argent >= ameliorations[numero].cost)
        {
            proprietaire.argent -= ameliorations[numero].cost;
            GameObject nTour = (GameObject)Instantiate(ameliorations[numero].newBuilding, transform.parent);
            nTour.transform.position = transform.position;
            nTour.GetComponent<Tour>().camp = camp;
            Destroy(gameObject);
        }
        else
        {
            throw new ArgumentOutOfRangeException("Vous n'avez pas assez d'argent pour faire evoluer cette tour");
        }
    }

    internal float distance (GameObject cible)
    {
        return ((Vector2)transform.position - (Vector2)cible.transform.position).magnitude;
    }

    public void uniteMorte()
    {
        unitesEnVie--;
    }
}