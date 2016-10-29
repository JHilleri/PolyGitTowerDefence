using UnityEngine;
using System.Collections;

public class PlacementTour : MonoBehaviour
{
    public Color originalColor;
    public Color cantPlaceColor;
    public Element element;
    public Joueur player;

    private SpriteRenderer spriteRenderer;

    private bool lastPlaceableState = true;

    public bool isPlaceable
    {
        get {
            bool isPlaceable = player.isTowerPlaceable(gameObject.transform.position, element);
            if(isPlaceable != lastPlaceableState)
            {
                spriteRenderer.color = (isPlaceable) ? originalColor : cantPlaceColor;
                lastPlaceableState = isPlaceable;
            }
            return isPlaceable;
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = element.couleur;
    }

    void Update()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = position;
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
        if (isPlaceable && Input.GetMouseButtonDown(0))
        {
            player.buildTower(position, element);
            Destroy(gameObject);
        }
    }
}