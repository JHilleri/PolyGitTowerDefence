using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlacementBaraquement : MonoBehaviour
{
    public Joueur player;

    public GameObject barracks;
    private GameObject sBarracks;

    public Toggle eau;
    public Toggle feu;
    public Toggle air;
    public Toggle terre;
    public Toggle plante;
    public Element[] tableau;

    public int camp;

    // Use this for initialization
    void Start()
    {
        if (Object.FindObjectOfType<Partie>().typePartie == 0)
        {
            camp = 0;
        }
        else
        {
            camp = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawn()
    {
        //if (joueur.place == false)
        // {
        sBarracks = Instantiate(barracks);
        sBarracks.transform.position = Input.mousePosition;
        sBarracks.GetComponent<suiviSouris>().element = choosedElement();
        sBarracks.GetComponent<suiviSouris>().player = player;
        // }
    }

    Element choosedElement()
    {
        if (eau.isOn)
        {
            return tableau[0];
        }
        if (feu.isOn)
        {
            return tableau[1];
        }
        if (air.isOn)
        {
            return tableau[2];
        }
        if (terre.isOn)
        {
            return tableau[3];
        }
        if (plante.isOn)
        {
            return tableau[4];
        }
        return null;
    }
}
