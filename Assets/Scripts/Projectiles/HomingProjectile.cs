using UnityEngine;
using System.Collections;
using System;

public class HomingProjectile : Projectile
{
    public override Unite Target
    {
        set
        {
            target = value;
        }
    }

    protected Unite target;

    public override void FixedUpdate()
    {
        if(!Pause.isPaused)
        {
            if (target == null || !target.isActiveAndEnabled)
                Destroy(gameObject);
            else
                transform.Translate((target.transform.position - transform.position).normalized * speed);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var unit = other.GetComponent<Unite>();
        if (unit != null)
        {
            if (unit == target)
            {
                unit.receiveDamages(damages, element);
                unit.addEffects(effectsToApply);
                Destroy(gameObject);
            }
        }
    }
}
