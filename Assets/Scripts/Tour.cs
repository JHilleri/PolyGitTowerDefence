using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Tour : MonoBehaviour {

    public GameObject cible;
    public GameObject projectile;
    public int camp; // définie le camp auquel la tour appartient 1 -> gauche, 2 -> droite
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

    // Use this for initialization
    void Start () {
        compteur = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
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
                            if (pCible.camp != camp && (minDist == -1 || dist < minDist))
                            {
                                cible = pCible.gameObject;
                                minDist = dist;
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

    void tir()
    {
        GameObject proj = Instantiate(projectile);
        proj.GetComponent<Projectile>().element = element;
        proj.GetComponent<SpriteRenderer>().color = element.couleur;
        DeplacementProjectile script = proj.GetComponent<DeplacementProjectile>();
        proj.transform.position = transform.position;
        Projectile projScript = proj.GetComponent<Projectile>();
        projScript.camp = camp;
        script.objX = cible.transform.position.x;
        script.objY = cible.transform.position.y;
        script.speed = projectSpeed;
    }

    float distance (GameObject cible)
    {
        return Mathf.Sqrt((transform.position.x-cible.transform.position.x)*(transform.position.x - cible.transform.position.x) + (transform.position.y - cible.transform.position.y)* (transform.position.y - cible.transform.position.y));
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