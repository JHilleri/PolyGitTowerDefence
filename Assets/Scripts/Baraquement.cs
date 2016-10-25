using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Baraquement : MonoBehaviour {

    public GameObject unitToSpawn;
    public uint nbUnitToSpawnPerRound;
    public uint spawnInterval;
    public int camp; // définit le camp auquel la tour appartient 1 -> gauche, 2 -> droite
    public int cout; // Cout de la caserne
    public int chemin; // assignation d'un chemin aux unités générées
    public int etape;
    public Element element;
    public Sprite image;
    public Sprite imageCouleur;
    public int intervalle;
    public List<EvolutionBatiment> ameliorations;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer colorSpriteRenderer;
    private GameObject menu;
    private bool menuActif;
    private Joueur proprietaire;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
        menu = transform.GetChild(1).gameObject;
        menu.SetActive(false);
        menuActif = false;
        choixChemin();
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
	}

    void OnMouseDown()
    {
        menu.SetActive(true);
        menuActif = true;
    }

    void choixChemin()
    {
        PointPassage[] points = FindObjectsOfType<PointPassage>();
        int etapeAPrendre = 0;
        int cheminAPrendre = 0;
        float distPoint;
        float minDistPoint = 20;
        foreach (PointPassage point in points)
        {
            distPoint = calcDistance(point.gameObject);
            if (distPoint < minDistPoint)
            {
                cheminAPrendre = point.numeroChemin;
                minDistPoint = distPoint;
                etapeAPrendre = point.ordre;
            }
        }
        chemin = cheminAPrendre;
        etape = etapeAPrendre;
    }

    public void evolue(int numero)
    {
        if (proprietaire.argent > ameliorations[numero].prixAmelioration)
        {
            GameObject nTour = Instantiate(ameliorations[numero].nouvelleTour);
            nTour.transform.position = transform.position;
            nTour.GetComponent<Tour>().camp = camp;
            Destroy(gameObject);
        }
        else
        {
            throw new ArgumentOutOfRangeException("Vous n'avez pas assez d'argent pour faire evoluer ce baraquement");
        }
    }

    float calcDistance(GameObject other)
    {
        return ((Vector2)other.transform.position - (Vector2)transform.position).magnitude;
    }
}