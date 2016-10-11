using UnityEngine;
using System.Collections;

public class DeplacementProjectile : MonoBehaviour {

    public float objX;
    public float objY;
    public float speed;
    private float speedX;
    private float speedY; 

	// Use this for initialization
	void Start () {
        speedX = objX - transform.position.x;
        speedY = objY - transform.position.y;
        float normalise = Mathf.Sqrt(speedX*speedX + speedY*speedY)/speed;
        speedX /= normalise;
        speedY /= normalise;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(speedX, speedY, 0);
	}
}
