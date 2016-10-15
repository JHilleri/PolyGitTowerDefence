using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Projectile : MonoBehaviour, Pausable {

    public int damage;
    public Element element;
    public int camp;

    public Vector2 target;
    public float speed;
    private Vector2 direction;
    private bool isPaused = false;

    void Start()
    {
        direction = (target - (Vector2)transform.position).normalized;
    }

    void FixedUpdate()
    {
        if(!isPaused)
        {
            transform.Translate(direction * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Soldat soldat = other.gameObject.GetComponent<Soldat>();
        if (soldat != null && soldat.camp != camp)
        {
            soldat.degat(soldat.element.lireRatioDegat(element) * damage);
            Destroy(gameObject);
        }
    }

    public void OnPauseGame()
    {
        isPaused = true;
    }

    public void OnResumeGame()
    {
        isPaused = false;
    }
}
