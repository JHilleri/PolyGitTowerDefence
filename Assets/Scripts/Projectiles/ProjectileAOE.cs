using UnityEngine;
using System.Collections;

public class ProjectileAOE : Projectile {

    public ZoneEffet effet;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        effet.camp = camp;
        effet.origine = gameObject;
        effet.active();
    }
}
