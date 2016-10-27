using UnityEngine;
using System.Collections;

public class MenuContextuelButton : MonoBehaviour {

    public int buttonIndex;
    private MenuContextuel menu;

    void Start()
    {
        menu = GetComponentInParent<MenuContextuel>();
    }

    void OnMouseDown()
    {
        menu.click(buttonIndex);
    }

    void OnMouseEnter()
    {
        menu.mouseEnter(buttonIndex);
    }

    void OnMouseExit()
    {
        menu.mouseExit(buttonIndex);
    }
}
