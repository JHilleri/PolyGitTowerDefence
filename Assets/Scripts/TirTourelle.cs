using UnityEngine;
using System.Collections;

public class TirTourelle : MonoBehaviour {

    public GameObject cible;
    public GameObject projectile;
    public float projectSpeed;
    public int intervalle;
    public int portee;
    private int compteur;

	// Use this for initialization
	void Start () {
        compteur = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(compteur < intervalle)
        {
            compteur++;
        }
        else
        {
            if(cible == null)
            {
                Soldat[] cibles = Object.FindObjectsOfType<Soldat>();
                if(cibles.Length >= 1)
                {
                    cible = cibles[0].gameObject;
                    float minDist = distance(cible);
                    foreach (Soldat pCible in cibles)
                    {
                        float dist = distance(pCible.gameObject);
                        if (dist < minDist)
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

    void tir()
    {
        GameObject proj = Instantiate(projectile);
        DeplacementProjectile script = proj.GetComponent<DeplacementProjectile>();
        proj.transform.position = transform.position;
        script.objX = cible.transform.position.x;
        script.objY = cible.transform.position.y;
        script.speed = projectSpeed;
    }

    float distance (GameObject cible)
    {
        return Mathf.Sqrt((transform.position.x-cible.transform.position.x)*(transform.position.x - cible.transform.position.x) + (transform.position.y - cible.transform.position.y)* (transform.position.y - cible.transform.position.y));
    }
}