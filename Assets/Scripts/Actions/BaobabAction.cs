using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(menuName = "Actions/BaobabAction")]
public class BaobabAction : BasicShooterAction {
    private const string BAOBAB_EFFECT_NAME = "baobabCatch";

    public float damages;

    private Unite enDigestion;

    protected override bool isReady()
    {
        return (base.isReady() && enDigestion == null);
    }

    public void catchUnit (Unite catchedUnit)
    {
        enDigestion = catchedUnit;
        catchedUnit.transform.position = Owner.transform.position;
        var effect = CreateInstance<Effect>();
        effect.stun = true;
        effect.name = BAOBAB_EFFECT_NAME;
        effect.damage = damages;
        effect.haveDuration = false;
        catchedUnit.addEffect(effect);
    }

    public override void haveHit(Unite unit)
    {
        if (enDigestion == null) catchUnit(unit);
    }

    protected override void searchTarget()
    {
        List<Collider2D> nearbyEntitys = new List<Collider2D>(Physics2D.OverlapCircleAll(Owner.transform.position, range));
        nearbyEntitys.RemoveAll(collider => {
            Unite unitScript = collider.GetComponent<Unite>();
            if (unitScript == null) return true;
            if (unitScript.haveEffect(BAOBAB_EFFECT_NAME)) return true;
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
}
