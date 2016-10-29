using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Actions/BasicShooter")]
public class BasicShooterAction : RangedAction
{
    public Projectile firedProjectil;

    protected Unite target;

    public override object Clone()
    {
        BasicShooterAction clone = CreateInstance<BasicShooterAction>();

        clone.range = range;
        clone.firedProjectil = firedProjectil;
        clone.reloadDuration = reloadDuration;
        clone.element = element;

        return clone;
    }

    public override void run()
    {
        if (reload++ == 0)
        {
            if (target == null || Vector2.Distance(Owner.transform.position, target.transform.position) > range)
            {
                searchTarget();
            }
            if (target != null) fire(target.transform.position);
        }
        else
        {
            if (reload >= reloadDuration) reload = 0;
        }
    }

    private void searchTarget()
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

    private void fire(Vector2 targetLocation)
    {
        Projectile projectil = Instantiate(firedProjectil);
        projectil.transform.parent = Owner.transform;
        projectil.transform.position = Owner.transform.position;
        projectil.target = target.transform.position;
        projectil.Element = element;
        projectil.camp = Owner.GetComponentInParent<Joueur>().camp;
        projectil.portee = 2 * range;

    }
}