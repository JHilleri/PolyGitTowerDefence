using UnityEngine;
using System.Collections;
using System;

public class Tour : MonoBehaviour{

    public GameObject cible;
    public GameObject projectileToFire;
    public int camp; // définit le camp auquel la tour appartient 1 -> gauche, 2 -> droite
    public int cout;// Cout de la tourelle
    public bool buf_allie;
    public bool tir_ennemi;
    public bool spawner;
    public bool projectile_obstacle;
    public GameObject uniteSpawn;
    public int intervalleSpawn;
    public int unitesMax;
    public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    public float projectSpeed;
    public int intervalle;
    public int portee;
    internal SpriteRenderer colorSpriteRenderer;
    internal int compteur;
    internal int compteurSpawn;
    internal int unitesEnVie;
    internal GameObject menu;
    internal bool menuActif;
    internal bool stopTirs;
    public AudioClip sonProjectile;

    // Use this for initialization
    internal virtual void Start () {
        compteur = 0;
        compteurSpawn = 0;
        unitesEnVie = 0;
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
        menu = GetComponentInChildren<MenuContextuelTour>().gameObject;
        menu.SetActive(false);
        menuActif = false;
        stopTirs = false;
    }
	
	// Update is called once per frame
	internal virtual void FixedUpdate () {
        if (menuActif)
        {
            if (Input.GetMouseButtonDown(0))
            {
                menuActif = false;
            }
        }
        else if (menu != null)
        {
            menu.SetActive(false);
        }
        if (!Pause.isPaused)
        {
            if (compteur < intervalle)
            {
                compteur++;
            }
            else if (!stopTirs)
            {
                Soldat[] cibles = UnityEngine.Object.FindObjectsOfType<Soldat>();
                if (cibles.Length >= 1)
                {
                    float dist = 0;
                    float minDist = portee + 1; // on initialise la la distance minimale de tir supérieur à la portée de la tour (un cas ou cela ne tire pas si aucune cible est à portée) 
                    foreach (Soldat pCible in cibles)
                    {
                        dist = distance(pCible.gameObject);
                        if(tir_ennemi)
                        {
                            if (pCible.camp != camp && dist < minDist)
                            {
                                cible = pCible.gameObject;
                                minDist = dist;
                            }
                        }

                        if(buf_allie)
                        {
                            if (pCible.camp == camp && dist < minDist)
                            {
                                cible = pCible.gameObject;
                                minDist = dist;
                            }
                        }
                    }
                    if (minDist > portee)
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

    void OnMouseDown()
    {
        menu.SetActive(true);
        menuActif = true;
    }

    void tir()
    {
        GameObject firedProjectile = Instantiate(projectileToFire);
        firedProjectile.GetComponent<SpriteRenderer>().color = element.couleur;
        firedProjectile.transform.position = transform.position;
        firedProjectile.transform.parent = transform;
        Projectile script = firedProjectile.GetComponent<Projectile>();
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
        script.portee = portee * 2;
        script.tour = this;
        if (sonProjectile != null)
        {
            AudioSource.PlayClipAtPoint(sonProjectile, Vector3.one, 1);
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