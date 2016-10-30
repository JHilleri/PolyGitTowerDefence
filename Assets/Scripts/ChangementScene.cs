using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour{

    public string nomScene;

    public void chargeScene()
    {
        SceneManager.LoadScene(nomScene);
    }

}
