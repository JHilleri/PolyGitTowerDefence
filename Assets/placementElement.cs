using UnityEngine.UI;
using System.Collections;

public class placementElement {

    public Button button;
    public Toggle eau;
    public Toggle feu;
    public Toggle air;
    public Toggle terre;
    public Toggle plante; 
    private Element element = new Element();


    // Use this for initialization
    void Start () {
        if (eau.isActiveAndEnabled)
        {
            element.nom = "eau";
        }
        if (feu.isActiveAndEnabled)
        {
            element.nom = "feu";
        }
        if (air.isActiveAndEnabled)
        {
            element.nom = "air";
        }
        if (terre.isActiveAndEnabled)
        {
            element.nom = "terre";
        }
        if (plante.isActiveAndEnabled)
        {
            element.nom = "plante";
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
