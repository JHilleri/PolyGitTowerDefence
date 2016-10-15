using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Joueur : MonoBehaviour {
	
	public int vie;
	public int argent;
	public int experience;
	public int camp;
	public Text vieText;
	public Text argentText;

    public GameObject basicTower;
    public GameObject basicBarrack;
    public Collider2D area;

    public LayerMask unbuildableLayers;
    private GameObject towerCreatorCursor;

	// Use this for initialization
	void Start () {
		vieText = GameObject.FindGameObjectsWithTag("VieJoueur")[0].GetComponent<Text>();
        vieText.text = vie.ToString();
		argentText = GameObject.FindGameObjectsWithTag("Argent")[0].GetComponent<Text>();
        argentText.text = argent.ToString();

        towerCreatorCursor = new GameObject("towerCreatorCursor");
        BoxCollider2D towerCollider = towerCreatorCursor.AddComponent<BoxCollider2D>();
        towerCollider.offset = basicTower.GetComponent<BoxCollider2D>().offset;
        towerCollider.size = basicTower.GetComponent<BoxCollider2D>().size;
        towerCollider.isTrigger = true;
        towerCreatorCursor.AddComponent<Rigidbody2D>().isKinematic = true;
        towerCreatorCursor.transform.parent = transform;
    }
	
	public void SetCamp(int c) {
		camp = c;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private GameObject build(GameObject building, Vector2 position)
    {
        GameObject newBuilding = Instantiate(building);
        newBuilding.transform.parent = transform;
        newBuilding.transform.position = position;
        return newBuilding;
    }

    public void buildTower(Vector2 position, Element element)
    {
        if(!isTowerPlassable(position)) throw new System.ArgumentOutOfRangeException();
        build(basicTower, position).GetComponent<Tour>().element = element;
    }
    public void buildBarrack(Vector2 position, Element element)
    {
        throw new System.NotImplementedException();
    }

    public bool isTowerPlassable(Vector2 position)
    {
        towerCreatorCursor.transform.position = position;
        return (area.OverlapPoint(position) && !towerCreatorCursor.GetComponent<Collider2D>().IsTouchingLayers(unbuildableLayers));
    }
}
