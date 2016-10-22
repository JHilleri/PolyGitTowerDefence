using UnityEngine;
using System.Collections;

public class ProjectileBaobab : Projectile {



    internal override void OnTriggerEnter2D(Collider2D other)
    {
        Soldat soldat = other.gameObject.GetComponent<Soldat>();
        if (soldat != null)
        {
            if (tir_ennemi && soldat.camp != camp)
            {
                foreach (Effect effect in effects)
                {
                    soldat.addEffect(effect);
                }
                soldat.degat(soldat.element.lireRatioDegat(element) * damage);
                if(tour is TourBaobab)
                {
                    TourBaobab tourB = (TourBaobab)tour;
                    tourB.digere(soldat);
                }
                Destroy(gameObject);
            }
        }
    }
}
