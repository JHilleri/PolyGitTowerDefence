using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Element element;
    public Action actionModel;
    public EvolutionBatiment[] ameliorations;
    private Action action;
    private GameObject menu;

    public virtual void Start()
    {
        action = (Action)actionModel.Clone();
        action.Owner = gameObject;

        menu = (GameObject)Instantiate(Resources.Load("MenuContextuel"), transform);
        menu.transform.position = transform.position;
        menu.SetActive(false);

        if (GetComponent<Collider2D>() == null) throw new MissingComponentException("a 2d Collider is required");
    }

    public virtual void FixedUpdate()
    {
        if (!Pause.isPaused)
        {
            action.run();
        }
    }

    public void evolve(int numero)
    {
        throw new NotImplementedException();
    }

    public void OnMouseDown()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
