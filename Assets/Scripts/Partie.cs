using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Partie : MonoBehaviour {

	public int typePartie;//Solo/gauche (0), multi/droit (1)
    public Configs configs;
	public Joueur joueurGauche;
	public Joueur joueurDroit;
    Text pdvAdverse;
	// Use this for initialization
	void Start () {
        pdvAdverse = GameObject.FindGameObjectsWithTag("VieAdversaire")[0].GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		if(typePartie==0) {
			pdvAdverse.text = joueurDroit.vie.ToString();
            joueurGauche.vieText.text = joueurGauche.vie.ToString();
            joueurGauche.argentText.text = joueurGauche.argent.ToString();
        }
        else
        {
            pdvAdverse.text = joueurGauche.vie.ToString();
            joueurDroit.vieText.text = joueurDroit.vie.ToString();
            joueurDroit.argentText.text = joueurDroit.argent.ToString();
        }
	}
}
