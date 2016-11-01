using UnityEngine;
using System.Collections;

public class ChangementSceneTouche : ChangementScene {

    public string touche;

	void Update () {
        if (Input.GetKeyDown(touche))
        {
            chargeScene();
        }
	}
}
