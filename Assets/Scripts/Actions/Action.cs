using UnityEngine;
using System.Collections;

public abstract class Action : ScriptableObject, System.ICloneable {
    private GameObject owner = null;
    private Joueur player = null;
    public Element element;
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

    public abstract object Clone();
    public abstract void run();
}
