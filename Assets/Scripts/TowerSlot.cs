using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TowerSlot : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler{
    public TowerPattern towerPattern;

    private Text textArea;
    private Image image;

    private Vector3 defaultPosition;
    // Use this for initialization
    void Start () {
        textArea = GetComponentInChildren<Text>();
        image = GetComponentsInChildren<Image>()[1];
        if(towerPattern)
        {
            textArea.text = towerPattern.description;
            image.sprite = towerPattern.sprite;
            defaultPosition = image.transform.position;

            image.sprite = towerPattern.sprite;
            textArea.text = towerPattern.description;
            image.color = towerPattern.element.couleur;
        }
    }
	
	// Update is called once per frame
	/*void Update () {
        
    }*/

    public bool readyToBuild()
    {
        return (towerPattern);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(readyToBuild())
        {

        }
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(readyToBuild())
        {
            image.transform.position = Input.mousePosition;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (readyToBuild())
        {
            image.transform.position = defaultPosition;
        }
    }
}
