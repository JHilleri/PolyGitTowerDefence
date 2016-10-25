using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MenuContextuelTour : MonoBehaviour {

    private Tour tour;
    private Text desc;
    private EvolutionBatiment[] ameliorations;
    Configs config;


    // Use this for initialization
    void Start () {
        tour = GetComponentInParent<Tour>();
        config = FindObjectOfType<Partie>().configs;
        GetComponent<SpriteRenderer>().sprite = config.fondMenuContextuel;
        transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVendreMenuContextuel;
        ameliorations = tour.ameliorations.ToArray();
        if (ameliorations.Length > 5)
        {
            throw new ArgumentOutOfRangeException("Une tour ne peut avoir que 5 améliorations possibles au maximum !");
        }
        for (int index = 0; index<5; index++)
        {
            if (index < ameliorations.Length && ameliorations[index].imageAmelioration != null)
            {
                transform.GetChild(index).gameObject.GetComponent<SpriteRenderer>().sprite = ameliorations[index].imageAmelioration;
            }
            else
            {
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
            }
        }
        desc = GameObject.FindGameObjectWithTag("Description").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click (int numero)
    {
        if (numero < ameliorations.Length && ameliorations[numero].nouvelleTour != null)
        {
            tour.evolue(numero);
        }
        if (numero == 5)
        {
            Destroy(tour.gameObject);
        }
    }

    public void mouseEnter (int numero)
    {
        if (numero < ameliorations.Length && ameliorations[numero].texteDescriptif != null)
        {
            desc.text = ameliorations[numero].texteDescriptif;
        }
        if (numero == 5)
        {
            desc.text = "Vendre la tour";
        }
    }

    public void mouseExit (int numero)
    {
        desc.text = "";
    }
}
