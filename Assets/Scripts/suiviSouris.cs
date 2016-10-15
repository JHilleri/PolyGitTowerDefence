using UnityEngine;
using System.Collections;

public class suiviSouris : MonoBehaviour
{

    public GameObject tourelleAir;
    public GameObject tourelleEau;
    public GameObject tourelleFeu;
    public GameObject tourellePlante;
    public GameObject tourelleTerre;
    public Color originalColor;
    public Color cantPlaceColor;
    public Element element;
    public Joueur player;

    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;
    private bool lastPlassableState = true;

    public bool isPlassable
    {
        get {
            bool isPlassable = player.isTowerPlassable(gameObject.transform.position);
            if(isPlassable != lastPlassableState)
            {
                spriteRenderer.color = (isPlassable) ? originalColor : cantPlaceColor;
                lastPlassableState = isPlassable;
            }
            return isPlassable;
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = element.couleur;
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = position;
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
        if (isPlassable && Input.GetMouseButtonDown(0))
        {
            player.buildTower( position, element);
            Destroy(gameObject);
        }
    }
}