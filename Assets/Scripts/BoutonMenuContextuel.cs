using UnityEngine;
using System.Collections;

public class BoutonMenuContextuel : MonoBehaviour {

    public int numeroBouton;
    private MenuContextuel menu;

	// Use this for initialization
	void Start ()
    {
        menu = GetComponentInParent<MenuContextuel>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnMouseDown()
    {
        menu.click(numeroBouton);
    }

    void OnMouseEnter()
    {
        menu.mouseEnter(numeroBouton);
    }

    void OnMouseExit()
    {
        menu.mouseExit(numeroBouton);
    }
}
