using UnityEngine;
using System;

public class ColoredBuilding : MonoBehaviour {
    public Sprite spriteToColor;
	public void Start () {
        if(spriteToColor != null)
        {
            var building = GetComponent<Building>();
            if (building == null) throw new MissingComponentException("Building component missing");
            var renderObject = new GameObject("colorRender");
            renderObject.transform.parent = gameObject.transform;
            renderObject.transform.position = gameObject.transform.position;
            var render = renderObject.AddComponent<SpriteRenderer>();
            render.sprite = spriteToColor;
            render.color = building.element.couleur;
        }
	}
	
}
