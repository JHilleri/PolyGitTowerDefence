using UnityEngine;
using System.Collections;
using System;

[System.Flags] public enum EffectType
{
    none,
    buff,
    debuff,
    both = buff | debuff
}

[CreateAssetMenu()]
public class Effect : ScriptableObject, ICloneable{
    public float speedAbsoluteModifier = 0;
    public float speedRelativeModifier = 1;
    public float hpAbsoluteModifier = 0;
    public float hpRelativeModifier = 1;
    public float attackModifier = 1;
    public float damage = 0;
    public EffectType canRemove = EffectType.none;

    public int duration = 1;
    public bool haveDuration = true;
    public EffectType type = EffectType.none;

    public Effect Clone()
    {
        Effect clone = CreateInstance<Effect>();
        clone.duration = duration;
        clone.haveDuration = haveDuration;
        clone.name = name;
        clone.type = type;
        clone.speedAbsoluteModifier = speedAbsoluteModifier;
        clone.speedRelativeModifier = speedRelativeModifier;
        clone.hpAbsoluteModifier = hpAbsoluteModifier;
        clone.hpRelativeModifier = hpRelativeModifier;
        clone.attackModifier = attackModifier;
        clone.damage = damage;
        clone.canRemove = canRemove;
        return clone;
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
