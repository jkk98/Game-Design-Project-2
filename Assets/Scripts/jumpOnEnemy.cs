﻿using UnityEngine;
using System.Collections;

public class jumpOnEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		Debug.Log ("NIGGER");
		if (col.gameObject.tag == "enemy") {
			Destroy (col.gameObject);
		}
	}
}
