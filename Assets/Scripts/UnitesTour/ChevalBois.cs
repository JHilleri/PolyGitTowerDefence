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
            stopRecherches = true;
            transform.SetParent(objectif.transform);
            attache = objectif.GetComponent<Soldat>();
            attache.vieMax += vieMax;
            baseVie = attache.getVie();
            attache.degat(-vie);
            attache.normalSpeed += vitesseAjoutee;
            attache.monte = true;
            attache.monture = this;
            transform.position = attache.transform.position;
            attache.recalculeTrajectoireVitesse();
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
            attache.normalSpeed -= vitesseAjoutee;
            attache.monte = false;
            attache.monture = null;
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
