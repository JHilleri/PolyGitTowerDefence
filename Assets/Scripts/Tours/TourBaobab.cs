using UnityEngine;
using System.Collections;

public class TourBaobab : Tour {

    public float degat;
    public int intervalleDegat;

    private Unite enDigestion;
    private int compteurDigestion;

    protected override void Start()
    {
        base.Start();
        compteurDigestion = 0;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        if (Pause.isPaused)
        {
            if (compteurDigestion < intervalleDegat)
            {
                compteurDigestion++;
            }
            else if (enDigestion != null)
            {
                compteurDigestion = 0;
                enDigestion.degat(enDigestion.element.lireRatioDegat(element) * degat);
            }
            else
            {
                stopTirs = false;
            }
        }
	}

    public void digere (Soldat ennemi)
    {
        enDigestion = ennemi;
        ennemi.transform.position = transform.position;
        ennemi.occupe = true;
        ennemi.paralise = true;
        stopTirs = true;
    }
}
