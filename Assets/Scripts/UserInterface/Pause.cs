using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    private static  bool pause = false;
    public static bool isPaused
    {
        set { pause = value; }
        get { return pause; }
    }
    public KeyCode pauseKey;

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
            togglePause();
    }

    public static void togglePause()
    {
        isPaused = !pause;
    }
}
