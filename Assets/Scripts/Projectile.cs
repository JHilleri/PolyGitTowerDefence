using UnityEngine;
using System.Collections;
using System;

[System.Flags]public enum TargetType
{
    none = 0,
    enemy = 1,
    ally = 2,
    both = enemy | ally
}

public class Projectile : MonoBehaviour {

    public int damage;
    public Tour tour;
    public Element element;
    public int camp;
    public TargetType targetType = TargetType.enemy;
    public bool debuf_ennemie_repouse;
    public float windEffectPower;
    public bool eclair_en_chaine;
    private int numProjectile = 1;
    public int maxChaineProjectile = 1;
    public bool terre_obstacle;
    public int PV;
    public bool terre_boulet_persistant;
    public Effect[] effects;
    public Vector2 target;
    public float speed;
    public float portee;
    
    public GameObject projectileAdditionnel; // utilisé seulement pour l'éclair en chaine
    public int porteeChaine;
    public AudioClip sonProjectile;
    public bool attaquable = false; // attention ne doit pas être modifié dans les prefab (juste pour que tour/soldat puissent le savoir)
    private GameObject cible = null;
    protected Vector2 direction;
    private float distance_totale;
    private float distance_parcourue = 0;

    public Element Element {
        get { return element; }
        set {
            element = value;
            GetComponent<SpriteRenderer>().color = element.couleur;
        }
    }

    protected virtual void Start()
    {
        direction = (target - (Vector2)transform.position).normalized;
        distance_totale = ((Vector2)transform.position - target).magnitude;
    }

    void FixedUpdate()
    {
        if(!Pause.isPaused)
        {
            if (terre_obstacle && distance_parcourue >= distance_totale)
            {
                attaquable = true;
                if (PV < 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.Translate(direction * speed);
                distance_parcourue += speed;
            }
            if (!terre_boulet_persistant && !terre_obstacle && distance(target) > portee) Destroy(gameObject);
            else if(terre_boulet_persistant && distance(target) > (3 * portee)) Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Soldat soldat = other.gameObject.GetComponent<Soldat>();
        if(soldat != null)
        {
            if( ((targetType & TargetType.enemy) != 0 && soldat.camp != camp) || ((targetType & TargetType.ally) != 0 && soldat.camp == camp) )
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
				Destroy(gameObject);
            }
        }
        //ne rentre jamais ici !
        else // si en contact avec le projectile d'obstacle
        {
            Projectile proj = other.gameObject.GetComponent<Projectile>();
            if (proj != null) // toujours == nul
            {
                if ((targetType & TargetType.enemy) != 0 && proj.camp != camp && proj.attaquable)
                {
                    proj.PV -= (int)(proj.element.lireRatioDegat(element) * damage);
                    Destroy(gameObject);
                }
            }
        }
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

                if (pCible.camp != camp && dist < minDist && dist > 0.35)
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
        if (cible != null && numProjectile < maxChaineProjectile)
        {
            tir();
        }
    }

    void tir()
    {
        if (projectileAdditionnel != null)
        {
            GameObject projectileTire = Instantiate(projectileAdditionnel);
            projectileTire.transform.position = transform.position;
            projectileTire.transform.parent = transform.parent;
            Projectile script = projectileTire.GetComponent<Projectile>();
            script.element = element;
            script.camp = camp;
            script.target = cible.transform.position;
            script.speed = speed;
            script.portee = porteeChaine;
            script.porteeChaine = porteeChaine;
            script.tour = tour;
            script.eclair_en_chaine = true;
            script.numProjectile = numProjectile + 1;
            script.maxChaineProjectile = maxChaineProjectile;
            script.targetType = targetType;
            script.sonProjectile = sonProjectile;
            script.damage = damage;
            script.projectileAdditionnel = projectileAdditionnel;
            if (sonProjectile != null)
            {
                AudioSource.PlayClipAtPoint(sonProjectile, Vector3.one, 1);
            }
        }
    }
}
