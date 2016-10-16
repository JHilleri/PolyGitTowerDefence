using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlacementTour : MonoBehaviour
{
    public Joueur player;

    public GameObject turret;
    private GameObject sTurret;

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
        sTurret = Instantiate(turret);
        sTurret.transform.position = Input.mousePosition;
        sTurret.GetComponent<suiviSouris>().element = choosedElement();
        sTurret.GetComponent<suiviSouris>().player = player;
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
