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

	// Use this for initialization
	void Start () {
		vieText = GameObject.FindGameObjectsWithTag("VieJoueur")[0].GetComponent<Text>();
        vieText.text = vie.ToString();
		argentText = GameObject.FindGameObjectsWithTag("Argent")[0].GetComponent<Text>();
        argentText.text = argent.ToString();
		
	}
	
	public void SetCamp(int c) {
		camp = c;
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
