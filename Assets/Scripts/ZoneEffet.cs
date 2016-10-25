using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class ZoneEffet : ScriptableObject {

    public float degat;
    public float rayon;
    public Element element;
    public int camp;
    public GameObject origine;

	public void active () {
        Soldat[] listeSoldats = FindObjectsOfType<Soldat>();
        foreach(Soldat soldat in listeSoldats)
        {
            if (soldat.camp != camp && distance(origine,soldat.gameObject) < rayon)
            {
                soldat.degat(soldat.element.lireRatioDegat(element) * degat);
            }
        }
	}

    public float distance (GameObject a, GameObject b)
    {
        return ((Vector2)a.transform.position - (Vector2)b.transform.position).magnitude;
    }
}
