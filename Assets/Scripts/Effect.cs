using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu()]
public class Effect : ScriptableObject, ICloneable{
    public float speedAbsoluteModifier = 0;
    public float speedRelativeModifier = 1;
    public float hpAbsoluteModifier = 0;
    public float hpRelativeModifier = 1;
    public float attackModifier = 1;
    public int duration = 1;
    public bool haveDuration = true;

    public Effect Clone()
    {
        Effect clone = CreateInstance<Effect>();
        clone.duration = duration;
        clone.haveDuration = haveDuration;
        clone.name = name;
        clone.speedAbsoluteModifier = speedAbsoluteModifier;
        clone.speedRelativeModifier = speedRelativeModifier;
        clone.hpAbsoluteModifier = hpAbsoluteModifier;
        clone.hpRelativeModifier = hpRelativeModifier;
        clone.attackModifier = attackModifier;
        return clone;
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
