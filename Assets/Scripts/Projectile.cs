﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int dgt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Soldat soldat = other.gameObject.GetComponent<Soldat>();
        if (soldat != null)
        {
            soldat.degat(dgt);
            Destroy(gameObject);
        }
    }
}
