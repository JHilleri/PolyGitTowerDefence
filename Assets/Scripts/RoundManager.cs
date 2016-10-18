using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {

    private Round currentRound;
    public int nbRounds;

	void Start () {
        startNewRound();
	}
	
	// Update is called once per frame
	void Update () {
	    if(currentRound.isEnd())
        {
            startNewRound();
        }
	}

    void startNewRound()
    {
        currentRound = gameObject.AddComponent<Round>();
        ++nbRounds;
    }
}
