using UnityEngine;
using System.Collections;

public abstract class RangedAction : Action {
    public float range;
    public int reloadDuration;

    protected int reload = 0;
}
