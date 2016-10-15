using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlacementTour : MonoBehaviour
{

    public GameObject turret;
    private GameObject sTurret;

    public Toggle eau;
    public Toggle feu;
    public Toggle air;
    public Toggle terre;
    public Toggle plante;
    public Element[] tableau;
    private Element element;

    public int camp;

    // Use this for initialization
    void Start()
    {
        if (Object.FindObjectOfType<Partie>().typePartie == 0)
        {
            camp = 1;
        }
        else
        {
            camp = 2;
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
        sTurret.GetComponent<suiviSouris>().element = choosedColor();
        sTurret.GetComponent<suiviSouris>().camp = camp;
        // }
    }

    Element choosedColor()
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
