using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
    public float damages;
    public Element element;
    public TargetType targetsType = TargetType.enemy;
    public Effect[] effectsToApply;
    public float speed;
    public float range;
    public AudioClip firedAudioClip;

    private Joueur player;
    private BasicShooterAction thrower;
    protected Vector2 startPosition;
    

    public Joueur Player
    {
        get { return player; }
        set { player = value; }
    }

    public abstract Unite Target { set; }

    public BasicShooterAction Thrower
    {
        get{ return thrower;}
        set{ thrower = value; }
    }

    public virtual void Awake()
    {
        startPosition = transform.position;
        if(firedAudioClip != null) AudioSource.PlayClipAtPoint(firedAudioClip, transform.position, 1);
    }

    public abstract void FixedUpdate();
    private void OnTriggerEnter2D(Collider2D other)
    {
        var unit = other.GetComponent<Unite>();
        if (unit != null && isTarget(unit))
        {
            Thrower.haveHit(unit);
            onTargetReached(unit);
        }
    }

    protected virtual bool isTarget(Unite unit)
    {
        if (unit == null) return false;
        return ((targetsType & TargetType.enemy) != 0 && unit.camp != Player.camp) || ((targetsType & TargetType.ally) != 0 && unit.camp == Player.camp);
    }

    protected abstract void onTargetReached(Unite target);
}
