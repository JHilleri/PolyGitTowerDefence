using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class ZoneEffet : ScriptableObject {

    public float degat;
    public float rayon;
    public Element element;
    public int camp;
    public Tour tour;

	public void active () {
        Soldat[] listeSoldats = FindObjectsOfType<Soldat>();
        foreach(Soldat soldat in listeSoldats)
        {
            if (soldat.camp != camp && tour.distance(soldat.gameObject) < rayon)
            {
                soldat.degat(soldat.element.lireRatioDegat(element) * degat);
            }
        }
	}
}
