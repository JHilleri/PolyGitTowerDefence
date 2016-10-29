using UnityEngine;
using System.Collections.Generic;

public class Round : MonoBehaviour {

    private class UnitToSpawn
    {
        public Unite unit;
        public Vector2 position;
        public uint timeBeforSpawn;
        public Joueur player;
        public Element element;
        public PointPassage path;
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
        Barrack[] barracks = GameObject.FindObjectsOfType<Barrack>();
        foreach (var barrack in barracks)
        {
            for(uint index = 0; index < barrack.nbUnitToSpawnPerRound; ++index)
            {
                UnitToSpawn unitToSpawnToAdd = new UnitToSpawn();
                unitToSpawnToAdd.unit = barrack.unitToSpawn;
                unitToSpawnToAdd.position = barrack.transform.position;
                unitToSpawnToAdd.player = barrack.Player;
                unitToSpawnToAdd.element = barrack.element;
                unitToSpawnToAdd.path = barrack.Path;
                unitToSpawnToAdd.timeBeforSpawn = index * barrack.spawnInterval;
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
            if (isEnd()) {
                startRound();
                foreach(var player in GameObject.FindGameObjectsWithTag("Player")) player.GetComponent<Joueur>().argent += 25;
            }
        }
    }

    bool tryToSpawnUnit(UnitToSpawn unitToTryToSpawn)
    {
        if(unitToTryToSpawn.timeBeforSpawn <= time)
        {
            Unite newUnit = Instantiate(unitToTryToSpawn.unit);
            newUnit.transform.position = unitToTryToSpawn.position;
            newUnit.camp = unitToTryToSpawn.player.camp;
            newUnit.transform.parent = unitsContainer.transform;
            newUnit.GetComponent<Soldat>().element = unitToTryToSpawn.element;
            newUnit.GetComponent<Soldat>().chemin = unitToTryToSpawn.path.numeroChemin;
            newUnit.GetComponent<Soldat>().etape = unitToTryToSpawn.path.ordre;
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
