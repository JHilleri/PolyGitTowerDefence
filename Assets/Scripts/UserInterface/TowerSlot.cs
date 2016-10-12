using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TowerSlot : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler{
    public TowerPattern towerPattern;
    public GameObject draggedTower;
    public GameObject towerToAdd;

    private Text textArea;
    private Image image;
    

    private Vector3 defaultPosition;
    private GameObject draggedTowerSprite;
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
	void Update () {
        
    }

    public bool readyToBuild()
    {
        return (towerPattern && draggedTower && towerToAdd);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(readyToBuild())
        {
            draggedTowerSprite = GameObject.Instantiate(draggedTower);
            draggedTowerSprite.transform.parent = GameObject.Find("Towers").transform;
        }
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(readyToBuild())
        {
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (readyToBuild())
        {
            GameObject.Destroy(draggedTowerSprite);
            if(draggedTowerSprite.GetComponent<TowerDragged>().plassable)
            {
                GameObject newTower = Instantiate<GameObject>(towerToAdd);
                newTower.transform.parent = GameObject.Find("Towers").transform;
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = newTower.transform.parent.position.z;
                newTower.transform.position = position;
            }
        }
    }
}
