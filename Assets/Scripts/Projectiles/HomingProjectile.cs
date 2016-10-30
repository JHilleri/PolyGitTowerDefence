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


    protected override void onTargetReached(Unite target)
    {
        target.receiveDamages(damages, element);
        target.addEffects(effectsToApply);
        Destroy(gameObject);
    }

    protected override bool isTarget(Unite unit)
    {
        return (unit != null && unit == target);
    }
}
