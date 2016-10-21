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
    public float windEffectPower;
    public bool buf_debuf_eau;

    public Effect[] effects;
    public Vector2 target;
    public float speed;
    private Vector2 direction;
    private bool isPaused = false;
    public int portee;
    

    void Start()
    {
        direction = (target - (Vector2)transform.position).normalized;
    }

    void FixedUpdate()
    {

        if(!isPaused)
        {
            transform.Translate(direction * speed);
            if (distance(target) > portee) Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Soldat soldat = other.gameObject.GetComponent<Soldat>();
        if(soldat != null)
        {
            if(tir_ennemi && soldat.camp != camp || !tir_ennemi && soldat.camp == camp)
            {
                foreach (Effect effect in effects)
                {
                    soldat.addEffect(effect);
                }
                soldat.degat(soldat.element.lireRatioDegat(element) * damage);
                if (debuf_ennemie_repouse)
                {
                    soldat.transform.Translate((float)0.5 * direction.x,(float)0.5*direction.y, 0);
                }
                else Destroy(gameObject);
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

    float distance(Vector2 cible)
    {
        return ((Vector2)transform.position - cible).magnitude;
    }
}
