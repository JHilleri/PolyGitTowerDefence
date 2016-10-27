using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {
    static public Text description
    {
        get { return GameObject.FindGameObjectWithTag("Description").GetComponent<Text>();}
    }
}
