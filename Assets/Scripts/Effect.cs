using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu()]
public class Effect : ScriptableObject{
    public float speedAbsoluteModifier;
    public float speedRelativeModifier = 1;
    public float attackModifier = 1;
    public int duration;
    public bool haveDuration;

    public Effect Clone()
    {
        Effect clone = CreateInstance<Effect>();
        clone.duration = duration;
        clone.haveDuration = haveDuration;
        clone.name = name;
        clone.speedAbsoluteModifier = speedAbsoluteModifier;
        clone.speedRelativeModifier = speedRelativeModifier;
        clone.attackModifier = attackModifier;
        return clone;
    }

}
