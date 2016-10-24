using UnityEngine;
using System.Collections;
using System;

public class Treant : UniteTour {


    internal override bool conditionsSpeciales(Soldat sol)
    {
        return sol.occupe == false;
    }

    internal override void action()
    {
        if (objectif.GetComponent<Soldat>().occupe == false)
        {
            objectif.GetComponent<Soldat>().setCible(this);
            objectif.GetComponent<Soldat>().occupe = true;
            stopRecherches = true;
            objectifAction = objectif;
        }
        else
        {
            objectif = null;
        }
    }

    internal override void objectifMort()
    {
        stopRecherches = false;
    }

    internal override void meurt()
    {
        if (objectif != null)
        {
            objectif.GetComponent<Soldat>().occupe = false;
        }
        base.meurt();
    }
}
