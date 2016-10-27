using UnityEngine;
using System.Collections;
using System;

public class MenuContextuel : MonoBehaviour {
    private Building building;
    Configs config;


    void Start()
    {
        building = GetComponentInParent<Building>();
        config = FindObjectOfType<Partie>().configs;
        GetComponent<SpriteRenderer>().sprite = config.fondMenuContextuel;
        transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVendreMenuContextuel;
        if (building.ameliorations.Length > 5)
        {
            throw new ArgumentOutOfRangeException("Une construction ne peut avoir que 5 améliorations possibles au maximum !");
        }
        for (int index = 0; index < 5; index++)
        {
            if (index < building.ameliorations.Length && building.ameliorations[index].imageAmelioration != null)
            {
                transform.GetChild(index).gameObject.GetComponent<SpriteRenderer>().sprite = building.ameliorations[index].imageAmelioration;
            }
            else
            {
                //transform.GetChild(index).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
                transform.GetChild(index).gameObject.SetActive(false);
            }
        }
    }


    public void click(int numero)
    {
        if (numero < building.ameliorations.Length && building.ameliorations[numero].nouvelleTour != null)
        {
            building.evolve(numero);
        }
        if (numero == 5)
        {
            Destroy(building.gameObject);
        }
    }

    public void mouseEnter(int numero)
    {
        if (numero < building.ameliorations.Length && building.ameliorations[numero].texteDescriptif != null)
        {
            Description.description.text = building.ameliorations[numero].texteDescriptif;
        }
        if (numero == 5)
        {
            Description.description.text = "Vendre la tour";
        }
    }

    public void mouseExit(int numero)
    {
        Description.description.text = "";
    }
}
