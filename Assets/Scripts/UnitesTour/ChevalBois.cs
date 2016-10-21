using UnityEngine;
using System.Collections;

public class ChevalBois : UniteTour {

    public float vitesseAjoutee;

    private Soldat attache;
    private float baseVie;

    internal override bool conditionsSpeciales(Soldat sol)
    {
        return sol.monte == false;
    }

    internal override void action()
    {
        if (objectif.GetComponent<Soldat>().monte == false)
        {
            cibleAllies = false;
            cibleEnnemis = false;
            transform.SetParent(objectif.transform);
            attache = objectif.GetComponent<Soldat>();
            attache.vieMax += vieMax;
            baseVie = attache.getVie();
            attache.degat(-vie);
            attache.vitesse += vitesseAjoutee;
            attache.monte = true;
            transform.position = attache.transform.position;
        }
        else
        {
            objectif = null;
        }
    }

    internal override void Update()
    {
        base.Update();
        if(attache != null && attache.getVie() < baseVie)
        {
            attache.vieMax -= vieMax;
            attache.vitesse -= vitesseAjoutee;
            attache.monte = false;
            meurt();
        }
        if (attache != null)
        {
            switch (attache.getDirection())
            {
                case 1:
                    spriteRenderer.sprite = imageDroite;
                    break;
                case 2:
                    spriteRenderer.sprite = imageGauche;
                    break;
                case 3:
                    spriteRenderer.sprite = imageDos;
                    break;
                case 4:
                    spriteRenderer.sprite = imageFace;
                    break;
            }
        }
    }
}
