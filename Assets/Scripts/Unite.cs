using UnityEngine;
using System.Collections.Generic;

[System.Flags]
public enum TargetType
{
    none = 0,
    enemy = 1,
    ally = 2,
    both = enemy | ally
}

public abstract class Unite : MonoBehaviour {

    public Element element;
    public float maxHitPoints;
    protected float hitPoints;
    public float speed;
    public float damages;
    public int camp;

    protected float effectiveHitPoints;
    protected float effectiveMaxHitPoints;
    protected float effectiveSpeed;
    protected float effectivesDamages;

    protected bool isStun = false;

    protected List<Effect> effects = new List<Effect>();

    protected virtual void Start()
    {
        resetEffectiveStats();
    }

    protected virtual void FixedUpdate()
    {
        if(!Pause.isPaused)
        {
            resetEffectiveStats();
            applyEffects();
        }
    }

    public virtual void degat(float dgt)
    {
        hitPoints -= dgt;
    }

    protected void applyEffect(Effect effect)
    {
        effectiveSpeed *= effect.speedRelativeModifier;
        effectiveSpeed += effect.speedAbsoluteModifier;
        effectivesDamages *= effect.attackModifier;
        effectiveMaxHitPoints *= effect.hpRelativeModifier;
        effectiveHitPoints *= effect.hpRelativeModifier;

        effectiveHitPoints -= effect.damage;
        isStun = (isStun || effect.stun);
    }

    public void addEffect(Effect effectToAdd)
    {
        if(effectToAdd.canRemove != EffectType.none)
        {
            effects.RemoveAll(effect => ((effect.type & effectToAdd.canRemove) != 0));
        }
        Effect actualVersion = effects.Find(effect => (effect.name == effectToAdd.name));
        if (actualVersion == null) effects.Add(effectToAdd.Clone());
        else actualVersion = effectToAdd.Clone();
    }

    public void addEffects(Effect[] effects)
    {
        foreach(var effect in effects)
        {
            addEffect(effect);
        }
    }

    public void applyEffects()
    {
        if (effects.Count != 0)
        {
            effects.RemoveAll(effect => (effect.haveDuration && effect.duration <= 0));
            foreach (Effect effect in effects)
            {
                applyEffect(effect);
                if (effect.haveDuration) effect.duration--;
            }
        }
        hitPoints = effectiveHitPoints * maxHitPoints / effectiveMaxHitPoints;
    }

    public bool haveEffect(string effectName)
    {
        return effects.Exists(effect => (effect.name == effectName));
    }

    private void resetEffectiveStats()
    {
        effectiveHitPoints = hitPoints;
        effectiveMaxHitPoints = maxHitPoints;
        effectivesDamages = damages;
        effectiveSpeed = speed;
        isStun = false;
    }

    public void receiveDamages(float damages, Element damagesElement)
    {
        effectiveHitPoints -= this.element.lireRatioDegat(damagesElement) * damages;
        hitPoints = effectiveHitPoints * maxHitPoints / effectiveMaxHitPoints;
    }
}
