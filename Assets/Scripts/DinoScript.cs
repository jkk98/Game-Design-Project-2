using UnityEngine;
using System.Collections;

public class DinoScript : MonoBehaviour {
	bool facingRight = true;
	float move = -.75f;
	Vector3 walkAmount;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		walkAmount.x = move * Time.deltaTime;
		//Debug.Log (walkAmount.x);
		transform.Translate (walkAmount);
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		//Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "environment" || col.gameObject.name.Contains("stairs")) {
			Flip ();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		move *= -1;
		transform.localScale = theScale;
	}
}
