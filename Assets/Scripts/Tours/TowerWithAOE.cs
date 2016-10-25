using UnityEngine;
using System.Collections.Generic;

public class TowerWithAOE : Tour
{
    public Effect[] effectsToApply;
    public float damages;
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        if (!Pause.isPaused)
        {
            if (compteur++ >= intervalle)
            {
                var nearbyEntitys = UnityEngine.Physics2D.OverlapCircleAll(transform.position, range);
                foreach (var entity in nearbyEntitys)
                {
                    Unite unit = entity.gameObject.GetComponent<Unite>();
                    if (unit != null)
                    {
                        if ((((targetType & TargetType.enemy) != 0 && unit.camp != camp) || ((targetType & TargetType.ally) != 0 && unit.camp == camp)))
                        {
                            unit.receiveDamages(damages, element);
                            foreach (Effect effect in effectsToApply) unit.addEffect(effect);
                        }
                    }
                }
                compteur = 0;
            }
        }
    }
}
