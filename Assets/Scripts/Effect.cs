using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu()]
public class Effect : ScriptableObject{
    public float speedAbsoluteModifier;
    public float speedRelativeModifier;

    public int duration;
    public bool haveDuration;

    public Effect Clone()
    {
        Effect clone = new Effect();
        clone.duration = duration;
        clone.haveDuration = haveDuration;
        clone.name = name;
        clone.speedAbsoluteModifier = speedAbsoluteModifier;
        clone.speedRelativeModifier = speedRelativeModifier;
        return clone;
    }

}
