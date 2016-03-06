using UnityEngine;
using System.Collections;

public class head_trap : MonoBehaviour {

	bool close_mouth = false;
	Animator anim;
	int ticks = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (close_mouth == true && ticks < 5) {
			ticks += 1;
		} else if (close_mouth == true && ticks >= 5) {
			ticks += 1;
			transform.position = new Vector3 (transform.position.x, transform.position.y - .1f, transform.position.z);
		}
		if (ticks == 60) {
			Destroy (gameObject);
		}
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			close_mouth = true;
			anim.SetBool ("closeMouth", true);
		}
	}
}
