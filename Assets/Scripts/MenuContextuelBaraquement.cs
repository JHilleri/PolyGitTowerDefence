using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuContextuelBaraquement : MonoBehaviour
{

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
    private Baraquement baraquement;
    private Text desc;
    private Configs config;

    // Use this for initialization
    void Start()
    {
        baraquement = GetComponentInParent<Baraquement>();
        config = FindObjectOfType<Partie>().configs;
        GetComponent<SpriteRenderer>().sprite = config.fondMenuContextuel;
        transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVendreMenuContextuel;
        if (imageAmeliorationElement != null)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmeliorationElement;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
        }
        if (imageAmelioration1 != null)
        {
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration1;
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
        }
        if (imageAmelioration2 != null)
        {
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration2;
        }
        else
        {
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
        }
        if (imageAmelioration3 != null)
        {
            transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration3;
        }
        else
        {
            transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
        }
        if (imageAmelioration4 != null)
        {
            transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = imageAmelioration4;
        }
        else
        {
            transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = config.boutonVideMenuContextuel;
        }
        desc = GameObject.FindGameObjectWithTag("Description").GetComponent<Text>();
    }

        // Update is called once per frame
        void Update()
    {

    }

    public void click(int numero)
    {
        if (numero == 0 && ameliorationElement != null)
        {
            GameObject nBaraquement = Instantiate(ameliorationElement);
            nBaraquement.transform.position = transform.position;
            nBaraquement.GetComponent<Baraquement>().camp = baraquement.camp;
            Destroy(baraquement.gameObject);
        }
        else if (numero == 1 && amelioration1 != null)
        {
            GameObject nBaraquement = Instantiate(amelioration1);
            nBaraquement.transform.position = transform.position;
            nBaraquement.GetComponent<Baraquement>().camp = baraquement.camp;
            Destroy(baraquement.gameObject);
        }
        else if (numero == 2 && amelioration2 != null)
        {
            GameObject nBaraquement = Instantiate(amelioration2);
            nBaraquement.transform.position = transform.position;
            nBaraquement.GetComponent<Baraquement>().camp = baraquement.camp;
            Destroy(baraquement.gameObject);
        }
        else if (numero == 3 && amelioration3 != null)
        {
            GameObject nBaraquement = Instantiate(amelioration3);
            nBaraquement.transform.position = transform.position;
            nBaraquement.GetComponent<Baraquement>().camp = baraquement.camp;
            Destroy(baraquement.gameObject);
        }
        else if (numero == 4 && amelioration4 != null)
        {
            GameObject nBaraquement = Instantiate(amelioration4);
            nBaraquement.transform.position = transform.position;
            nBaraquement.GetComponent<Baraquement>().camp = baraquement.camp;
            Destroy(baraquement.gameObject);
        }
        else if (numero == 5)
        {
            Destroy(baraquement.gameObject);
        }
    }

    public void mouseEnter(int numero)
    {
        if (numero == 0)
        {
            desc.text = textAmeliorationElement;
        }
        else if (numero == 1)
        {
            desc.text = textAmelioration1;
        }
        else if (numero == 2)
        {
            desc.text = textAmelioration2;
        }
        else if (numero == 3)
        {
            desc.text = textAmelioration3;
        }
        else if (numero == 4)
        {
            desc.text = textAmelioration4;
        }
        else if (numero == 5)
        {
            desc.text = "Vendre le baraquement";
        }
    }

    public void mouseExit(int numero)
    {
        desc.text = "";
    }
}
