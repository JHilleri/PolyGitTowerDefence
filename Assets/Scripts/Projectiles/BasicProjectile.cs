using UnityEngine;

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
            if ( ((Vector2)transform.localPosition).magnitude > range )
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void onTargetReached(Unite target)
    {
        target.receiveDamages(damages, element);
        target.addEffects(effectsToApply);
        Destroy(gameObject);
    }
}
