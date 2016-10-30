using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Actions/CircularAOE")] public class CircularAOE : RangedAction {
    public Effect[] effectsToApply;
    public float damages;

    protected override void onReady()
    {
        var nearbyEntitys = UnityEngine.Physics2D.OverlapCircleAll(Owner.transform.position, range);
        foreach (var entity in nearbyEntitys)
        {
            var unit = entity.GetComponent<Unite>();
            if (unit != null)
            {
                if ((((targetsType & TargetType.enemy) != 0 && unit.camp != Player.camp) || ((targetsType & TargetType.ally) != 0 && unit.camp == Player.camp)))
                {
                    unit.receiveDamages(damages, element);
                    foreach (Effect effect in effectsToApply) unit.addEffect(effect);
                }
            }
        }
        if (nearbyEntitys.Length > 0) startToReload();
    }
}
