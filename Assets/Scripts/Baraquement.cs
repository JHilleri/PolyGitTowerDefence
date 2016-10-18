using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Baraquement : MonoBehaviour {

    public GameObject unitToSpawn;
    public uint nbUnitToSpawnPerRound;
    public uint spawnInterval;
    public int camp; // définit le camp auquel la tour appartient 1 -> gauche, 2 -> droite
    public int cout; // Cout de la caserne
	 public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    //public int intervalle;
	
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer colorSpriteRenderer;
    private bool paused;
    private GameObject menu;
    private bool menuActif;

    // Use this for initialization
    void Start () {
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
        /*if (!paused)
        {
            if (compteur < intervalle)
            {
                compteur++;
            }
            else
            {
                creationUnite();
                compteur = 0;
            }
        }*/
	}

    void OnMouseDown()
    {
        menu.SetActive(true);
        menuActif = true;
    }

    /*void creationUnite() // les unités créées se trouvent sur le baraquement (elles vont d'elles mêmes prendre leur chemin)
    {
        GameObject Unite = Instantiate(AjoutUnite);
        Unite.transform.parent = transform;
        Unite.transform.position = transform.position;
        Soldat script = Unite.GetComponent<Soldat>(); // l'unité est déployé mais n'a pas encore de classe
        script.element = element;
        script.camp = camp;
    }*/

    public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
}