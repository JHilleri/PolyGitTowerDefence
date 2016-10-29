using UnityEngine;
using System.Collections;

public abstract class UniteTour : Unite {

    public Action tour; // todo : make an invoker Action
    public bool cibleAllies;
    public bool cibleEnnemis;
    public bool stopRecherches;
    public float porteeTour;
    public float portee;
    public float detect;
    public float vitesse;
    public Sprite imageFace;
    public Sprite imageDos;
    public Sprite imageGauche;
    public Sprite imageDroite;

    protected GameObject objectif;
    protected GameObject objectifAction;
    protected float distanceX;
    protected float distanceY;
    protected float distance;
    protected float vitesseX;
    protected float vitesseY;
    protected SpriteRenderer spriteRenderer;

    // Use this for initialization
    protected override void Start () {
        hitPoints = maxHitPoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	protected override void FixedUpdate() {
        base.FixedUpdate();
        if (!Pause.isPaused)
        {
            if (objectif == null)
            {
                if (!stopRecherches)
                {
                    if (calcDistance(tour.Owner) < porteeTour)
                    {
                        Soldat[] soldats = FindObjectsOfType<Soldat>();
                        float minDist = detect + 1;
                        Soldat plusProche = null;
                        foreach (Soldat soldat in soldats)
                        {
                            if (calcDistance(soldat.gameObject) < minDist && ((soldat.camp == camp && cibleAllies) || (soldat.camp != camp && cibleEnnemis)) && conditionsSpeciales(soldat))
                            {
                                minDist = calcDistance(soldat.gameObject);
                                plusProche = soldat;
                            }
                        }
                        if (minDist < detect)
                        {
                            objectif = plusProche.gameObject;
                        }
                    }
                    else
                    {
                        objectif = tour.Owner;
                    }
                }
                if (objectifAction == null)
                {
                    objectifMort();
                }
            }
            else
            {
                distanceX = objectif.transform.position.x - transform.position.x;
                distanceY = objectif.transform.position.y - transform.position.y;
                distance = calcDistance(objectif);
                if (distance < portee)
                {
                    action();
                }
                else
                {
                    vitesseX = (distanceX / distance) * vitesse;
                    vitesseY = (distanceY / distance) * vitesse;
                    transform.Translate(vitesseX, vitesseY, 0);
                }
            }
            if (Mathf.Abs(vitesseX) > Mathf.Abs(vitesseY))
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
                if (vitesseY > 0)
                {
                    spriteRenderer.sprite = imageDos;
                }
                else
                {
                    spriteRenderer.sprite = imageFace;
                }
            }
            if (hitPoints <= 0)
            {
                meurt();
            }
        }
    }

    float calcDistance(GameObject cible)
    {
        return ((Vector2)transform.position - (Vector2)cible.transform.position).magnitude;
    }

    internal virtual bool conditionsSpeciales(Soldat sol)//A override pour des conditions spéciales sur les cibles
    {
        return true;
    }

    internal virtual void objectifMort()//A override pour des actions spéciales à la mort de la cible
    {

    }

    internal abstract void action();// A override dans les classes filles

    internal virtual void meurt()
    {
        //tour.uniteMorte();
        Destroy(gameObject);
    }
}
