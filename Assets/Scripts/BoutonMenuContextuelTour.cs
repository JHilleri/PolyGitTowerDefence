using UnityEngine;
using System.Collections;

public class BoutonMenuContextuelTour : MonoBehaviour {

    public int numeroBouton;
    private MenuContextuelTour menu;

	// Use this for initialization
	void Start ()
    {
        menu = GetComponentInParent<MenuContextuelTour>();
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
