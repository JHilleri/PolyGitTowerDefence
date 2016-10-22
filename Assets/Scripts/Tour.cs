using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Tour : MonoBehaviour, Pausable {

    public GameObject cible;
    public GameObject projectileToFire;
    public int camp; // définit le camp auquel la tour appartient 1 -> gauche, 2 -> droite
    public int cout;// Cout de la tourelle
    public bool buf_allie;
    public bool tir_ennemi;
    public bool spawner;
    public GameObject uniteSpawn;
    public int intervalleSpawn;
    public int unitesMax;
    public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    public float projectSpeed;
    public int intervalle;
    public int portee;
    //internal SpriteRenderer spriteRenderer;
    internal SpriteRenderer colorSpriteRenderer;
    internal int compteur;
    internal int compteurSpawn;
    internal int unitesEnVie;
    private bool paused;
    internal GameObject menu;
    internal bool menuActif;
    internal bool stopTirs;

    // Use this for initialization
    internal virtual void Start () {
        compteur = 0;
        compteurSpawn = 0;
        unitesEnVie = 0;
        //spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (!paused)
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
                    float minDist = portee + 1; // on initialise la la distance minimale de tir supérieur à la portée de la tour (un cas ou cela ne tire pas si aucune cible est à portée) 
                    foreach (Soldat pCible in cibles)
                    {
                        float dist = distance(pCible.gameObject);
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
        script.target = cible.transform.position;
        script.speed = projectSpeed;
        script.portee = portee * 2;
        script.tour = this;
    }

    internal float distance (GameObject cible)
    {
        return ((Vector2)transform.position - (Vector2)cible.transform.position).magnitude;
    }

    public void uniteMorte()
    {
        unitesEnVie--;
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