using UnityEngine;
using System.Collections.Generic;

public class ChainReaction : HomingProjectile {
    public int nbImpact;
    public float chainRange;

    private List<Unite> alreadyHitUnits = new List<Unite>();
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var unit = other.GetComponent<Unite>();
        if (unit != null)
        {
            if (unit == target)
            {
                unit.receiveDamages(damages, element);
                unit.addEffects(effectsToApply);
                if(nbImpact-- > 0)
                {
                    var nextTarget = findNextTarget();
                    if(nextTarget != null)
                    {
                        fireNextProjectile(nextTarget);
                    }
                }
                Destroy(gameObject);
            }
        }
    }

    private Unite findNextTarget()
    {
        Unite nextTarget = null;
        float distance = -1;
        List<Collider2D> nearbyEntitys = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, chainRange));
        foreach(var unitColider in nearbyEntitys)
        {
            var unit = unitColider.GetComponent<Unite>();
            if (unit != null)
            {
                if (unit != target && !alreadyHitUnits.Contains(unit) && (unit.camp != Player.camp && (targetsType & TargetType.enemy) != 0) || (unit.camp == Player.camp && (targetsType & TargetType.ally) != 0))
                {
                    if (distance < 0 || Vector2.Distance(transform.position, unit.transform.position) < distance)
                    {
                        nextTarget = unit;
                        distance = Vector2.Distance(transform.position, unit.transform.position);
                    }
                }
            }
        }
        return nextTarget;
    }

    private void fireNextProjectile(Unite nextTarget)
    {
        var nextProjectile = Instantiate(this, transform.parent) as ChainReaction;
        nextProjectile.Target = nextTarget;
        nextProjectile.Player = Player;
        nextProjectile.transform.parent = transform.parent;
        var coloredProjectile = nextProjectile.GetComponent<ColoredProjectile>();
        if (coloredProjectile != null)
        {
            coloredProjectile.updateColor();
        }
        nextProjectile.alreadyHitUnits.AddRange(alreadyHitUnits);
        nextProjectile.alreadyHitUnits.Add(target);
        nextProjectile.enabled = true;
        nextProjectile.GetComponent<CircleCollider2D>().enabled = true;
    }
}
