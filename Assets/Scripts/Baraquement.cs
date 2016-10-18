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
    public int chemin; // assignation d'un chemin aux unités générées
    public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    public int intervalle;
	
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
        chemin = choixChemin();
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

    int choixChemin()
    {
        PointPassage[] points = FindObjectsOfType<PointPassage>();
        int cheminAPrendre = 0;
        float distPoint;
        float minDistPoint = 100;
        foreach (PointPassage point in points)
        {
            distPoint = calcDistance(point.gameObject);
            if (distPoint < minDistPoint)
            {
                cheminAPrendre = point.numeroChemin;
                minDistPoint = distPoint;
            }
        }
        return cheminAPrendre;
    }

    float calcDistance(GameObject other)
    {
        return ((Vector2)other.transform.position - (Vector2)transform.position).magnitude;
    }
}