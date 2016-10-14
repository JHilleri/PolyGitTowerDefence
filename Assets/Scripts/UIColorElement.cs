using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIColorElement : MonoBehaviour {

    public Element element;

    // Use this for initialization
    void Start () {
        Image image = this.GetComponent<Image>();
        image.color = element.couleur;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
