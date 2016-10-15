using UnityEngine;
using System.Collections;

public class suiviSouris : MonoBehaviour
{

    public GameObject finalTurret;
    public Color originalColor;
    public Color cantPlaceColor;
    public Element element;
    private bool canPlace;
    private SpriteRenderer spriteRenderer;

    public int camp;

    // Use this for initialization
    void Start()
    {
        canPlace = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = element.couleur;
        finalTurret.GetComponent<Tour>().camp = camp;
        finalTurret.GetComponent<Tour>().element = element;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        pos.z = 0;
        transform.position = pos;
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(transform.root.gameObject);
        }
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            Instantiate(finalTurret, transform.position, transform.rotation);
            Destroy(transform.root.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        canPlace = false;
        spriteRenderer.color = cantPlaceColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        canPlace = false;
        spriteRenderer.color = cantPlaceColor;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canPlace = true;
        spriteRenderer.color = originalColor;
    }

}