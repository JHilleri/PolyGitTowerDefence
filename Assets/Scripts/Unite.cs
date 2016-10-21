using UnityEngine;
using System.Collections;

public abstract class Unite : MonoBehaviour {

    public Element element;
    public float vieMax;
    internal float vie; 

    public virtual void degat(float dgt)
    {
        vie -= dgt;
    }

}
