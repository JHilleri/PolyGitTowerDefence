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
    protected Vector2 startPosition;

    public Joueur Player
    {
        get { return player; }
        set { player = value; }
    }

    public abstract Unite Target { set; }

    public virtual void Awake()
    {
        startPosition = transform.position;
        if(firedAudioClip != null) AudioSource.PlayClipAtPoint(firedAudioClip, transform.position, 1);
    }

    public abstract void FixedUpdate();
    protected abstract void OnTriggerEnter2D(Collider2D other);
}
