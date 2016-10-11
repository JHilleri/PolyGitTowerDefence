using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public bool pause = false;

    public void setPause(bool newPauseState)
    {
        pause = newPauseState;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        if(newPauseState)
        {
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void togglePause()
    {
        setPause(!pause);
    }
}
