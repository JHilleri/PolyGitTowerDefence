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

    public virtual void Start()
    {
         player = GetComponentInParent<Joueur>();

        action = (Action)actionModel.Clone();
        action.Owner = gameObject;
        action.element = element;

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
            action.run();
        }
    }

    public void evolve(int numero)
    {
        if (player.argent >= ameliorations[numero].prixAmelioration)
        {
            player.argent -= ameliorations[numero].prixAmelioration;
            GameObject nextVersion = (GameObject)Instantiate(ameliorations[numero].nouvelleTour, transform.parent);
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
        menu.SetActive(!menu.activeSelf);
        menuState = true;
    }
}
