using UnityEngine;
using System.Collections;
using System;

public class BasicProjectile : Projectile
{
    private Vector2 direction;

    public Vector2 Direction
    {
        get{ return direction;}
        set{ direction = value;}
    }

    public override Unite Target
    {
        set{ Direction = (value.transform.position - transform.position).normalized; }
    }

    public override void FixedUpdate()
    {
        if (!Pause.isPaused)
        {
            transform.Translate(Direction * speed);
            if (Vector2.Distance(startPosition, transform.position) > range)
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var unit = other.GetComponent<Unite>();
        if (unit != null)
        {
            if(((targetsType & TargetType.enemy) != 0 && unit.camp != Player.camp) || ((targetsType & TargetType.ally) != 0 && unit.camp == Player.camp))
            {
                unit.receiveDamages(damages, element);
                unit.addEffects(effectsToApply);
                Destroy(gameObject);
            }
        }
    }
}
