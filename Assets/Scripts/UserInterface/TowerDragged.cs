using UnityEngine;
using System.Collections;

public class TowerDragged : MonoBehaviour {
    public Color disabledColor;
    public bool plassable;

    private SpriteRenderer spriteComponent;
    private Color originalColor;

	void Start () {
        spriteComponent = GetComponent<SpriteRenderer>();
        originalColor = spriteComponent.color;
        plassable = true;
    }
	
	void Update () {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = transform.parent.position.z;
        transform.position = position;
        spriteComponent.color = (plassable) ? originalColor : disabledColor;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        plassable = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        plassable = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        plassable = true;
    }
}
