﻿using UnityEngine;
using System.Collections;

public class TargetCollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter (Collision col)
    {
        Destroy (gameObject);
        Destroy(col.gameObject);
    }
}
