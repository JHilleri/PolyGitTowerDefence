using UnityEngine;
using System.Collections;

public class BoutonMenuContextuelBaraquement : MonoBehaviour {

    public int numeroBouton;
    private MenuContextuelBaraquement menu;

    // Use this for initialization
    void Start()
    {
        menu = GetComponentInParent<MenuContextuelBaraquement>();
    }

    // Update is called once per frame
    void Update()
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
