using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Projectile : MonoBehaviour, Pausable {

    public int damage;
    public Element element;
    public int camp;
    public bool buf_allie_vitesse;
    public bool debuf_ennemie_repouse;
    public bool tir_ennemi;


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
        //Si c'est une tour de buff
        if (buf_allie_vitesse)
        {
            if (soldat != null && soldat.camp == camp)
            {
                soldat.vitesse = soldat.vitesse * (float)1.2;
                Destroy(gameObject);
            }
        }
        if (tir_ennemi)
        {
            if (soldat != null && soldat.camp != camp)
            {
                if (debuf_ennemie_repouse)
                {
                    soldat.vitesse = -soldat.vitesse;
                }
                soldat.degat(soldat.element.lireRatioDegat(element) * damage);
                Destroy(gameObject);
            }
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
