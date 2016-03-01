using UnityEngine;
using System.Collections;

public class egg_collision : MonoBehaviour {

	int timeAlive;

	// Use this for initialization
	void Start () {
		timeAlive = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive += 1;
		if (timeAlive == 180) {
			Destroy (this.gameObject);
		}
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "enemy") {
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
		else if (col.gameObject.CompareTag("environment")) {
			Destroy (gameObject);
		}
	}
}
