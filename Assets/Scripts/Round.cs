using UnityEngine;
using System.Collections.Generic;

public class Round : MonoBehaviour {

    private class UnitToSpawn
    {
        public GameObject unit;
        public Vector2 position;
        public uint timeBeforSpawn;
        public int faction;
        public Element element;
        public int chemin;
        public int etape;
    }
    private List<UnitToSpawn> unitsToSpawn = new List<UnitToSpawn>();

    private GameObject unitsContainer;

    private uint time;
    public uint nbRound = 0;

    // Use this for initialization
    void Start() {
        unitsContainer = new GameObject("Units");
        unitsContainer.transform.parent = transform;
        startRound();
    }

    void startRound() {
        GameObject[] barracks = GameObject.FindGameObjectsWithTag("barrack");
        foreach (GameObject barrack in barracks)
        {
            Baraquement barrackScript = barrack.GetComponent<Baraquement>();
            for(uint index = 0; index < barrackScript.nbUnitToSpawnPerRound; ++index)
            {
                UnitToSpawn unitToSpawnToAdd = new UnitToSpawn();
                unitToSpawnToAdd.unit = barrackScript.unitToSpawn;
                unitToSpawnToAdd.position = barrack.transform.position;
                unitToSpawnToAdd.faction = barrackScript.camp;
                unitToSpawnToAdd.element = barrackScript.element;
                unitToSpawnToAdd.chemin = barrackScript.chemin;
                unitToSpawnToAdd.etape = barrackScript.etape;
                unitToSpawnToAdd.timeBeforSpawn = index * barrackScript.spawnInterval;
                unitsToSpawn.Add(unitToSpawnToAdd);
            }
        }
        ++nbRound;
        time = 0;
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            AI artificialIntelligence = player.GetComponent<AI>();
            if(artificialIntelligence != null)
            {
                artificialIntelligence.beginRound();
            }
        }
	}
	
	void FixedUpdate()
    {
        if (!Pause.isPaused)
        {
            unitsToSpawn.RemoveAll(unitToSpawn => tryToSpawnUnit(unitToSpawn));
            ++time;
            if (isEnd()) startRound();
        }
    }

    bool tryToSpawnUnit(UnitToSpawn unitToTryToSpawn)
    {
        if(unitToTryToSpawn.timeBeforSpawn <= time)
        {
            GameObject newUnit = Instantiate(unitToTryToSpawn.unit);
            newUnit.transform.position = unitToTryToSpawn.position;
            newUnit.GetComponent<Soldat>().camp = unitToTryToSpawn.faction;
            newUnit.transform.parent = unitsContainer.transform;
            newUnit.GetComponent<Soldat>().element = unitToTryToSpawn.element;
            newUnit.GetComponent<Soldat>().chemin = unitToTryToSpawn.chemin;
            newUnit.GetComponent<Soldat>().etape = unitToTryToSpawn.etape;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isEnd()
    {
        return (unitsContainer.GetComponentsInChildren<Soldat>().Length == 0 && unitsToSpawn.Count == 0);
    }
}
