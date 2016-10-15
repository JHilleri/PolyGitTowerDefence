using UnityEngine;
using System.Collections;

public class MenuContextuel : MonoBehaviour {

    public Sprite imageFond;
    public GameObject ameliorationElement;
    public GameObject amelioration1;
    public GameObject amelioration2;
    public GameObject amelioration3;
    public GameObject amelioration4;
    public Sprite imageAmeliorationElement;
    public Sprite imageAmelioration1;
    public Sprite imageAmelioration2;
    public Sprite imageAmelioration3;
    public Sprite imageAmelioration4;
    public string textAmeliorationElement;
    public string textAmelioration1;
    public string textAmelioration2;
    public string textAmelioration3;
    public string textAmelioration4;
    private Tour tour;


    // Use this for initialization
    void Start () {
        tour = GetComponentInParent<Tour>();
        if (imageAmeliorationElement != null)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmeliorationElement;
        }
        if (imageAmelioration1 != null)
        {
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration1;
        }
        if (imageAmelioration2 != null)
        {
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration2;
        }
        if (imageAmelioration3 != null)
        {
            transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration3;
        }
        if (imageAmelioration4 != null)
        {
            transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration4;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click (int numero)
    {
        if (numero == 0 && ameliorationElement != null)
        {
            GameObject nTour = Instantiate(ameliorationElement);
            nTour.transform.position = transform.position;
            nTour.GetComponent<Tour>().camp = tour.camp;
            Destroy(gameObject);
        }
        else if (numero == 1 && amelioration1 != null)
        {
            GameObject nTour = Instantiate(amelioration1);
            nTour.transform.position = transform.position;
            nTour.GetComponent<Tour>().camp = tour.camp;
            Destroy(gameObject);
        }
        else if (numero == 2 && amelioration2 != null)
        {
            GameObject nTour = Instantiate(amelioration2);
            nTour.transform.position = transform.position;
            nTour.GetComponent<Tour>().camp = tour.camp;
            Destroy(gameObject);
        }
        else if (numero == 3 && amelioration3 != null)
        {
            GameObject nTour = Instantiate(amelioration3);
            nTour.transform.position = transform.position;
            nTour.GetComponent<Tour>().camp = tour.camp;
            Destroy(gameObject);
        }
        else if (numero == 4 && amelioration4 != null)
        {
            GameObject nTour = Instantiate(amelioration4);
            nTour.transform.position = transform.position;
            nTour.GetComponent<Tour>().camp = tour.camp;
            Destroy(gameObject);
        }
        else if (numero == 5)
        {
            Destroy(gameObject);
        }
    }

    public void mouseEnter (int numero)
    {

    }

    public void mouseExit (int numero)
    {

    }
}
