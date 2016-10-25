using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MenuContextuelBaraquement : MonoBehaviour
{

    private Baraquement baraquement;
    private Text desc;
    private EvolutionBatiment[] ameliorations;
    Configs config;


    // Use this for initialization
    void Start()
    {
        baraquement = GetComponentInParent<Baraquement>();
        config = FindObjectOfType<Partie>().configs;
        GetComponent<SpriteRenderer>().sprite = config.fondMenuContextuel;
        transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVendreMenuContextuel;
        ameliorations = baraquement.ameliorations.ToArray();
        if (ameliorations.Length > 5)
        {
            throw new ArgumentOutOfRangeException("Un barraquement ne peut avoir que 5 améliorations possibles au maximum !");
        }
        for (int index = 0; index < 5; index++)
        {
            if (index < ameliorations.Length && ameliorations[index].imageAmelioration != null)
            {
                transform.GetChild(index).gameObject.GetComponent<SpriteRenderer>().sprite = ameliorations[index].imageAmelioration;
            }
            else
            {
                transform.GetChild(index).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
            }
        }
        desc = GameObject.FindGameObjectWithTag("Description").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void click(int numero)
    {
        if (numero < ameliorations.Length && ameliorations[numero].nouvelleTour != null)
        {
            baraquement.evolue(numero);
        }
        if (numero == 5)
        {
            Destroy(baraquement.gameObject);
        }
    }

    public void mouseEnter(int numero)
    {
        if (numero < ameliorations.Length && ameliorations[numero].texteDescriptif != null)
        {
            desc.text = ameliorations[numero].texteDescriptif;
        }
        if (numero == 5)
        {
            desc.text = "Vendre le barraquement";
        }
    }

    public void mouseExit(int numero)
    {
        desc.text = "";
    }
}
