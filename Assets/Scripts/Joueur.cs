using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts;
using System;

public class Joueur : MonoBehaviour{
	
	public int vie;
	public int argent;
	public int experience;
	public int camp;
	public Text vieText;
	public Text argentText;

    public PointPassage[] defaultsSpawn;

    public GameObject basicTower;
    public GameObject basicBarrack;
    public Collider2D area;

    public LayerMask unbuildableLayers;
    private GameObject towerCreatorCursor;
    private GameObject barrackCreatorCursor;

    public GameObject[] basicTowerList;
    private Dictionary<Element, GameObject> basicTowers;

    public GameObject[] basicBarrackList;
    private Dictionary<Element, GameObject> basicBarracks;

    // Use this for initialization
    void Start () {
		vieText = GameObject.FindGameObjectsWithTag("VieJoueur")[0].GetComponent<Text>();
        vieText.text = vie.ToString();
		argentText = GameObject.FindGameObjectsWithTag("Argent")[0].GetComponent<Text>();
        argentText.text = argent.ToString();

        barrackCreatorCursor = new GameObject("barrackCreatorCursor");
        BoxCollider2D barrackCollider = barrackCreatorCursor.AddComponent<BoxCollider2D>();
        barrackCollider.offset = basicBarrack.GetComponent<BoxCollider2D>().offset;
        barrackCollider.size = basicBarrack.GetComponent<BoxCollider2D>().size;
        barrackCollider.isTrigger = true;
        barrackCreatorCursor.AddComponent<Rigidbody2D>().isKinematic = true;
        barrackCreatorCursor.transform.parent = transform;


        basicBarracks = new Dictionary<Element, GameObject>();

        for (int i = 0; i < basicBarrackList.GetLength(0); i++)
        {
            Baraquement barrackScript = basicBarrackList[i].GetComponent<Baraquement>();
            basicBarracks.Add(barrackScript.element, basicBarrackList[i]);
        }

        towerCreatorCursor = new GameObject("towerCreatorCursor");
        BoxCollider2D towerCollider = towerCreatorCursor.AddComponent<BoxCollider2D>();
        towerCollider.offset = basicTower.GetComponent<BoxCollider2D>().offset;
        towerCollider.size = basicTower.GetComponent<BoxCollider2D>().size;
        towerCollider.isTrigger = true;
        towerCreatorCursor.AddComponent<Rigidbody2D>().isKinematic = true;
        towerCreatorCursor.transform.parent = transform;


        basicTowers = new Dictionary<Element, GameObject>();

        for(int i = 0; i < basicTowerList.GetLength(0); i++)
        {
            Tour towerScript = basicTowerList[i].GetComponent<Tour>();
            basicTowers.Add(towerScript.element, basicTowerList[i]);
        }
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
        if(!isTowerPlaceable(position)) throw new System.ArgumentOutOfRangeException("position","tower can't be placed at that position");
        if (!basicTowers.ContainsKey(element)) throw new System.ArgumentOutOfRangeException("element", "the element " + element.nom + " isn't available");
        if (basicTower.GetComponentInChildren<Tour>().cout > argent) throw new System.ArgumentOutOfRangeException("cout", "Vous n\'avez pas assez d\'argent pour acheter cette tour");
        Tour towerScript = build(basicTowers[element], position).GetComponent<Tour>();
        towerScript.camp = camp;
        argent -= basicTower.GetComponentInChildren<Tour>().cout;
    }
    public void buildBarrack(Vector2 position, Element element)
    {
        if (!isBarrackPlaceable(position)) throw new System.ArgumentOutOfRangeException("position", "barrack can't be placed at that position");
        if (!basicBarracks.ContainsKey(element)) throw new System.ArgumentOutOfRangeException("element", "the element " + element.nom + " isn't available");
        if (basicBarrack.GetComponentInChildren<Baraquement>().cout > argent) throw new System.ArgumentOutOfRangeException("cout", "Vous n\'avez pas assez d\'argent pour acheter ce barraquement");
        Baraquement barrackScript = build(basicBarracks[element], position).GetComponent<Baraquement>();
        barrackScript.camp = camp;
        argent -= basicBarrack.GetComponentInChildren<Baraquement>().cout;
    }

    public bool isBarrackPlaceable(Vector2 position)
    {
        barrackCreatorCursor.transform.position = position;
        return (area.OverlapPoint(position) && !barrackCreatorCursor.GetComponent<Collider2D>().IsTouchingLayers(unbuildableLayers));
    }

    public bool isTowerPlaceable(Vector2 position)
    {
        towerCreatorCursor.transform.position = position;
        return (area.OverlapPoint(position) && !towerCreatorCursor.GetComponent<Collider2D>().IsTouchingLayers(unbuildableLayers));
    }
}
