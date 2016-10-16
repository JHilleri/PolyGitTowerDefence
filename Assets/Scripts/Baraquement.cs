using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Baraquement : MonoBehaviour {

    public GameObject AjoutUnite;
    public int camp; // définit le camp auquel la tour appartient 1 -> gauche, 2 -> droite
    public int cout; // Cout de la caserne
	public int classe = 3; // archer = 0 - brute = 1 - magicien = 2 - paysan = 3 - soldat = 4 - tank = 5, par défaut on produit des paysans
    public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    public int intervalle;
	
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
                creationUnite();
            }
        }
	}

    void OnMouseDown()
    {
        menu.SetActive(true);
        menuActif = true;
    }

    void creationUnite() // les unités créées se trouvent sur le baraquement (elles vont d'elles mêmes prendre leur chemin)
    {
        GameObject Unite = Instantiate(AjoutUnite);
        Unite.GetComponent<SpriteRenderer>().color = element.couleur;
        Unite.transform.parent = transform;
		Soldat script = Unite.GetComponent<Soldat>(); // l'unité est déployé mais n'a pas encore de classe
		script.classe = classe; // attribution de la classe du baraquement à l'unité tout juste créée
        script.element = element;
        script.camp = camp;
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