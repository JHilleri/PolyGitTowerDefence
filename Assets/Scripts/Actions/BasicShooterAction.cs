using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Actions/BasicShooter")]
public class BasicShooterAction : RangedAction
{
    public Projectile firedProjectile;

    protected Unite target;

    protected override void onReady()
    {
        if (target == null || Vector2.Distance(Owner.transform.position, target.transform.position) > range)
        {
            searchTarget();
        }
        if (target != null) fire(target);
    }

    protected virtual void searchTarget()
    {
        List<Collider2D> nearbyEntitys = new List<Collider2D>(Physics2D.OverlapCircleAll(Owner.transform.position, range));
        nearbyEntitys.RemoveAll(collider => {
            Unite unitScript = collider.GetComponent<Unite>();
            if ( unitScript == null) return true;
            return !((unitScript.camp != Player.camp && (targetsType & TargetType.enemy) != 0) || (unitScript.camp == Player.camp && (targetsType & TargetType.ally) != 0));
        });
        nearbyEntitys.Sort((entity1, entity2) =>
        {
            float distance1 = Vector2.Distance(entity1.transform.position, Owner.transform.position);
            float distance2 = Vector2.Distance(entity1.transform.position, Owner.transform.position);
            return (distance1 == distance2) ? 0 : (distance1 < distance2) ? -1 : 1;
        });
        target = (nearbyEntitys.Count > 0) ? nearbyEntitys[0].gameObject.GetComponent<Unite>() : null;
    }

    protected virtual void fire(Unite target)
    {
        Projectile projectile = Instantiate(firedProjectile, Owner.transform) as Projectile;
        projectile.transform.position = Owner.transform.position;
        projectile.Target = target;
        projectile.element = element;
        projectile.Player = Owner.GetComponentInParent<Joueur>();
        projectile.range = 2 * range;
        projectile.Thrower = this;
        var coloredProjectile = projectile.GetComponent<ColoredProjectile>();
        if (coloredProjectile != null)
        {
            coloredProjectile.updateColor();
        }
        startToReload();
    }
}