using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Tour : MonoBehaviour {

    public GameObject cible;
    public GameObject projectileToFire;
    public int camp; // définit le camp auquel la tour appartient 1 -> gauche, 2 -> droite
    public bool buf_allie;
    public bool tir_ennemi;
    public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    public float projectSpeed;
    public int intervalle;
    public int portee;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer colorSpriteRenderer;
    private int compteur;
    private bool paused;
    private GameObject menu;
    private bool menuActif;

    // Use this for initialization
    void Start () {
        compteur = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
        menu = transform.GetChild(1).gameObject;
        menu.SetActive(false);
        menuActif = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
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
            else
            {
                if (cible == null)
                {
                    Soldat[] cibles = UnityEngine.Object.FindObjectsOfType<Soldat>();
                    if (cibles.Length >= 1)
                    {
                        cible = null;
                        float minDist = -1;
                        foreach (Soldat pCible in cibles)
                        {
                            float dist = distance(pCible.gameObject);
                            if(tir_ennemi)
                            {
                                if (pCible.camp != camp && (minDist == -1 || dist < minDist))
                                                            {
                                                                cible = pCible.gameObject;
                                                                minDist = dist;
                                                            }
                            }

                            if(buf_allie)
                            {
                                if (pCible.camp == camp && (minDist == -1 || dist < minDist))
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
                }
                compteur = 0;
                if (cible != null)
                {
                    if (distance(cible) > portee)
                    {
                        cible = null;
                    }
                    else
                    {
                        tir();
                    }
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
    }

    float distance (GameObject cible)
    {
        return ((Vector2)transform.position - (Vector2)cible.transform.position).magnitude;
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