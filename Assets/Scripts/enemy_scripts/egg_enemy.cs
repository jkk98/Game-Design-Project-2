using UnityEngine;
using System.Collections;

public class egg_enemy : MonoBehaviour {

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
        //cannonBall ignore parent
        if(gameObject.name.Contains("cannonBall") && col.gameObject.name.Contains("ankylo"))
        {
            return;
        }
        //Subtract health from player
		if (col.gameObject.name.Contains("Character")) {
            Debug.Log("eggDamage");
            col.gameObject.GetComponent<EggScript>().damagePlayer();
			Destroy (gameObject);
		}
        //Destroy self on any other collision
		else if (col.gameObject.CompareTag("environment") || col.gameObject.CompareTag("floor") || 
			(col.gameObject.CompareTag("enemy") && !col.gameObject.name.Contains("oviraptor")
            && !col.gameObject.name.Contains("lakitu"))) {
			Destroy (gameObject);
		}
	}
}
