using UnityEngine;
using System.Collections;

public abstract class Action : ScriptableObject, System.ICloneable {
    private GameObject owner = null;
    private Joueur player = null;
    private int reload = 0;

    public Element element;
    public TargetType targetsType = TargetType.enemy;
    public int reloadDuration;
    
    public GameObject Owner
    {
        get { return owner; }
        set
        {
            owner = value;
            player = Owner.GetComponentInParent<Joueur>();
        }
    }

    public void startToReload()
    {
        reload = 1;
    }

    public Joueur Player
    {
        get{return player;}
    }

    public virtual object Clone()
    {
        return UnityEngine.Object.Instantiate(this);
    }

    public virtual void haveHit(Unite unit){}

    protected virtual bool isReady()
    {
        return (reload == 0);
    }

    protected abstract void onReady();

    public void run()
    {
        if (isReady()) onReady();
        else if (reload++ >= reloadDuration) reload = 0;
    }
}
