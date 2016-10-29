using UnityEngine;
using System.Collections;

public abstract class Action : ScriptableObject, System.ICloneable {
    private GameObject owner = null;
    private Joueur player = null;
    public Element element;
    public TargetType targetsType = TargetType.enemy;
    public GameObject Owner
    {
        get { return owner; }
        set
        {
            owner = value;
            player = Owner.GetComponentInParent<Joueur>();
        }
    }

    public Joueur Player
    {
        get{return player;}
    }

    public virtual object Clone()
    {
        return UnityEngine.Object.Instantiate(this);
    }
    public abstract void run();
}
