using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Soldat : MonoBehaviour, Pausable{

    public int camp; // 1 = gauche 2 = droite
    public int chemin;
    public int vieMax;
    public float vitesse;
    public float marge;
    public Sprite imageFace;
    public Sprite imageFaceCouleur;
    public Sprite imageDos;
    public Sprite imageDosCouleur;
    public Sprite imageGauche;
    public Sprite imageGaucheCouleur;
    public Sprite imageDroite;
    public Sprite imageDroiteCouleur;
    public Element element;
    private int vie;
    private int etape;
    private PointPassage objectif;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer colorSpriteRenderer;
    private float oldDistance;
    private float distanceX;
    private float distanceY;
    private float distance;
    private float vitesseX;
    private float vitesseY;
    private bool paused;

	// Use this for initialization
	void Start () {
        if (camp == 1)
        {
            etape = 0;
        }
        else
        {
            etape = pointMax(chemin);
        }
        objectif = null;
        vie = vieMax;
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorSpriteRenderer.color = element.couleur;
    }
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            if (objectif == null)
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
                        vitesseX = (distanceX / distance) * vitesse;
                        vitesseY = (distanceY / distance) * vitesse;
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
                    if(camp == 1)
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
                    if (Mathf.Abs(vitesseX) > Mathf.Abs(vitesseY))
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
                }
                oldDistance = distance;
                if (vie <= 0)
                {
                    meurt();
                }
            }
        }
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

    public void degat(int dgt)
    {
        vie -= dgt;
    }

    void gagne()
    {
        Destroy(gameObject);
    }

    void meurt()
    {
        Destroy(gameObject);
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
