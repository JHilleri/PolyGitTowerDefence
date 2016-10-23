using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Projectile : MonoBehaviour, Pausable {

    public int damage;
    public Tour tour;
    public Element element;
    public int camp;
    public bool tir_ennemi;
    public bool buf_allie; // le booléen est actif si le projectile est pour du buff de ses troupes (vitesse/cooldown/attaque/PV)
    public bool debuf_ennemie_repouse;
    public float windEffectPower;
    public bool buf_debuf_eau;
    public bool status; // si le projectile inflige un status (poison/paralysie/geler...)
    public bool eclair_en_chaine;
    public bool terre_obstacle;
    public int PV;
    public bool terre_boulet_persistant;
    public Effect[] effects;
    public Vector2 target;
    public float speed;
    public int portee;
    
    public GameObject projectileAdditionnel; // utilisé seulement pour l'éclair en chaine
    public int porteeChaine;
    public AudioClip sonProjectile;
    public bool attaquable = false; // attention ne doit pas être modifié dans les prefab (juste pour que tour/soldat puissent le savoir)
    private GameObject cible;
    internal Vector2 direction;
    private float distance_totale;
    private float distance_parcourue = 0;
    private bool isPaused = false;

    void Start()
    {
        direction = (target - (Vector2)transform.position).normalized;
        distance_totale = ((Vector2)transform.position - target).magnitude;
    }

    void FixedUpdate()
    {
        if(!isPaused)
        {
            if (terre_obstacle && distance_parcourue >= distance_totale)
            {
                attaquable = true;
            }
            else
            {
                transform.Translate(direction * speed);
                distance_parcourue += speed;
            }
            if (distance(target) > portee) Destroy(gameObject);
        }
    }

    internal virtual void OnTriggerEnter2D(Collider2D other)
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
                else if (eclair_en_chaine)
                {
                    chaineEclair();
                    Destroy(gameObject);
                }
				else if (terre_boulet_persistant)
				{
					
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

    // utilisée seulement par l'éclair en chaine (chaineEclair)
    internal float distance(GameObject cible)
    {
        return ((Vector2)transform.position - (Vector2)cible.transform.position).magnitude;
    }

    void chaineEclair()
    {
        Soldat[] cibles = UnityEngine.Object.FindObjectsOfType<Soldat>();
        if (cibles.Length > 1)
        {
            float dist = 0;
            float minDist = porteeChaine + 1;
            foreach (Soldat pCible in cibles)
            {
                dist = distance(pCible.gameObject);
  
                if (pCible.camp != camp && dist < minDist)
                {
                    cible = pCible.gameObject;
                    minDist = dist;
                }
            }
            if (minDist > porteeChaine)
            {
                cible = null;
            }
        }
        if (cible != null)
        {
            tir();
        }
    }

    void tir()
    {
        GameObject projectileTire = Instantiate(projectileAdditionnel);
        projectileTire.transform.position = transform.position;
        projectileTire.transform.parent = transform;
        Projectile script = projectileTire.GetComponent<Projectile>();
        script.element = element;
        script.camp = camp;
        script.target = cible.transform.position;
        script.speed = speed;
        script.portee = porteeChaine;
        script.porteeChaine = porteeChaine;
        script.tour = tour;
        script.eclair_en_chaine = true;
        script.tir_ennemi = true;
        script.sonProjectile = sonProjectile;
        script.damage = damage;
        script.projectileAdditionnel = projectileAdditionnel;
        if (sonProjectile != null)
        {
            AudioSource.PlayClipAtPoint(sonProjectile, Vector3.one, 1);
        }

    }
}
