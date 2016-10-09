using UnityEngine;
using System.Collections;

public class Soldat : MonoBehaviour {

    public int chemin;
    public int vieMax;
    public float vitesse;
    public float marge;
    public Sprite imageFace;
    public Sprite imageDos;
    public Sprite imageGauche;
    public Sprite imageDroite; 
    private int vie;
    private int etape;
    private pointPassage objectif;
    private SpriteRenderer spriteRenderer;
    private float distanceX;
    private float distanceY;
    private float distance;
    private float vitesseX;
    private float vitesseY;

	// Use this for initialization
	void Start () {
        etape = 0;
        objectif = null;
        vie = vieMax;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (objectif == null)
        {
            pointPassage[] points = Object.FindObjectsOfType<pointPassage>();
            bool found = false;
            int indexPoints = 0;
            while(indexPoints < points.Length && !found)
            {
                if (points[indexPoints].numeroChemin == chemin && points[indexPoints].ordre == etape)
                {
                    objectif = points[indexPoints];
                    distanceX = objectif.transform.position.x - transform.position.x;
                    distanceY = objectif.transform.position.y - transform.position.y;
                    distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
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
        distanceX = objectif.transform.position.x - transform.position.x;
        distanceY = objectif.transform.position.y - transform.position.y;
        distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
        if(distance < marge)
        {
            etape++;
            objectif = null;
        }
        else
        {
            transform.Translate(vitesseX, vitesseY, 0);
            if(Mathf.Abs(vitesseX) > Mathf.Abs(vitesseY))
            {
                if (vitesseX > 0)
                {
                    spriteRenderer.sprite = imageDroite;
                }
                else
                {
                    spriteRenderer.sprite = imageGauche;
                }
            }
            else
            {
                if(vitesseY > 0)
                {
                    spriteRenderer.sprite = imageDos;
                }
                else
                {
                    spriteRenderer.sprite = imageFace;
                }
            }
        }
        if(vie <= 0)
        {
            meurt();
        }
    }

    void gagne()
    {
        Destroy(transform.root.gameObject);
    }

    void meurt()
    {
        Destroy(transform.root.gameObject);
    }
}
