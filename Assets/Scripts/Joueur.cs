using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Joueur : MonoBehaviour{
	
	public int vie;
	public int argent;
	public int experience;
	public int camp;
	public Text vieText;
	public Text argentText;

    public PointPassage[] defaultsSpawn;

    public Collider2D area;

    public LayerMask unbuildableLayers;
    private GameObject cursor;
    private GameObject buildingsObject;

    public EvolutionBatiment[] basicTowerList;
    private Dictionary<Element, EvolutionBatiment> basicTowers;
    private BoxCollider2D towerCollider;

    public EvolutionBatiment[] basicBarrackList;
    private Dictionary<Element, EvolutionBatiment> basicBarracks;
    private bool isLoaded = false;

    void Start () {
		vieText = GameObject.FindGameObjectsWithTag("VieJoueur")[0].GetComponent<Text>();
        vieText.text = vie.ToString();
		argentText = GameObject.FindGameObjectsWithTag("Argent")[0].GetComponent<Text>();
        argentText.text = argent.ToString();

        basicBarracks = new Dictionary<Element, EvolutionBatiment>();

        for (int i = 0; i < basicBarrackList.GetLength(0); i++)
        {
            EvolutionBatiment barrackScript = basicBarrackList[i];
            basicBarracks.Add(barrackScript.newBuilding.element, basicBarrackList[i]);
        }

        cursor = new GameObject("towerCreatorCursor");
        towerCollider = cursor.AddComponent<BoxCollider2D>();
        towerCollider.isTrigger = true;
        cursor.AddComponent<Rigidbody2D>().isKinematic = true;
        cursor.transform.parent = transform;


        basicTowers = new Dictionary<Element, EvolutionBatiment>();

        for(int i = 0; i < basicTowerList.GetLength(0); i++)
        {
            EvolutionBatiment towerScript = basicTowerList[i];
            basicTowers.Add(towerScript.newBuilding.element, basicTowerList[i]);
        }

        buildingsObject = new GameObject("buildings");
        buildingsObject.transform.parent = transform;
        isLoaded = true;
    }
	
	public void SetCamp(int c) {
		camp = c;
	}

    private Building build(Building building, Vector2 position)
    {
        Building newBuilding = (Building)Instantiate(building, buildingsObject.transform);
        newBuilding.transform.position = position;
        return newBuilding;
    }

    public void buildTower(Vector2 position, Element element)
    {
        if(!isTowerPlaceable(position, element)) throw new System.ArgumentOutOfRangeException("position","tower can't be placed");
        build(basicTowers[element].newBuilding, position).GetComponent<Building>();
        argent -= basicTowers[element].cost;
    }
    public void buildBarrack(Vector2 position, Element element)
    {
        if (!isBarrackPlaceable(position, element)) throw new System.ArgumentOutOfRangeException("position", "barrack can't be placed");
        build(basicBarracks[element].newBuilding, position);
        argent -= basicBarracks[element].cost;
    }

    public bool isBarrackPlaceable(Vector2 position, Element element)
    {
        if (!isLoaded) return false;
        if (!basicBarracks.ContainsKey(element)) throw new System.ArgumentOutOfRangeException("element", "the element " + element.nom + " isn't available");
        return isBuildingPlaceable(position, basicBarracks[element]);
    }

    public bool isTowerPlaceable(Vector2 position, Element element)
    {
        if (!basicTowers.ContainsKey(element)) throw new System.ArgumentOutOfRangeException("element", "the element " + element.nom + " isn't available");
        return isBuildingPlaceable(position, basicTowers[element]);
    }

    public bool isBuildingPlaceable(Vector2 position, EvolutionBatiment upgrade)
    {
        if (upgrade.cost > argent) return false;
        cursor.transform.position = position;
        towerCollider.offset = upgrade.newBuilding.GetComponent<BoxCollider2D>().offset;
        towerCollider.size = upgrade.newBuilding.GetComponent<BoxCollider2D>().size;
        return (area.OverlapPoint(position) && !cursor.GetComponent<Collider2D>().IsTouchingLayers(unbuildableLayers));
    }
}
