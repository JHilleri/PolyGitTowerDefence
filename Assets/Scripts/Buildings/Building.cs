using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Element element;
    public Action actionModel;
    public EvolutionBatiment[] ameliorations;

    private Action action;
    private GameObject menu;
    private Joueur player;
    private bool menuState = false;

    public Joueur Player{
        get{return player;}
    }

    public virtual void Start()
    {
         player = GetComponentInParent<Joueur>();

        if(actionModel != null)
        {
            action = (Action)actionModel.Clone();
            action.Owner = gameObject;
            action.element = element;
        }
        

        menu = (GameObject)Instantiate(Resources.Load("MenuContextuel"), transform);
        menu.transform.position = transform.position;
        menu.SetActive(menuState);

        if (GetComponent<Collider2D>() == null) throw new MissingComponentException("a 2d Collider is required");
    }

    public virtual void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && menuState)
            menuState = false;
        else
            menu.SetActive(menuState);
        if (!Pause.isPaused)
        {
            if(action != null)action.run();
        }
    }

    public void evolve(int numero)
    {
        if (player.argent >= ameliorations[numero].cost)
        {
            player.argent -= ameliorations[numero].cost;
            GameObject nextVersion = (GameObject)Instantiate(ameliorations[numero].newBuilding, transform.parent);
            nextVersion.transform.position = transform.position;
            Destroy(gameObject);
        }
        else
        {
            throw new ArgumentOutOfRangeException("Vous n'avez pas assez d'argent pour faire evoluer cette tour");
        }
    }

    public void OnMouseDown()
    {
        if(FindObjectOfType<Partie>().joueurGauche.GetInstanceID() == Player.GetInstanceID())
        {
            menu.SetActive(!menu.activeSelf);
            menuState = true;
        }
    }
}
