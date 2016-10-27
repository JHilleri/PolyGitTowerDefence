using UnityEngine;

public class ColoredBuilding : Building {
    public Sprite spriteToColor;
	public override void Start () {
        base.Start();
        if(spriteToColor != null)
        {
            var renderObject = new GameObject("colorRender");
            renderObject.transform.parent = gameObject.transform;
            renderObject.transform.position = gameObject.transform.position;
            var render = renderObject.AddComponent<SpriteRenderer>();
            render.sprite = spriteToColor;
            render.color = element.couleur;
        }
	}
	
	public override void FixedUpdate() {
        base.FixedUpdate();
	}
}
