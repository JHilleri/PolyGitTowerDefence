using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu()]
public class Element : ScriptableObject
{
    public string nom;
    public Color couleur;
    public List<Element> listeBonus; //Elements sur lesquels cet élément a un bonus
    public List<Element> listeMalus; //Elements sur lesquels cet éléments a un malus

    public float lireRatioDegat(Element recu)//Renvoie le ratio de dégat subi par cet élément de la part de l'element recu
    {
        if (listeBonus.Contains(recu))
        {
            return 0.75F;
        }
        if (listeMalus.Contains(recu))
        {
            return 1.5F;
        }
        return 1.0F;
    }
}
