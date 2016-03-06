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
        //As soon as player lands on it stay still for five ticks and then destroy self after 20 ticks
		if (close_mouth == true && ticks < 5) {
			ticks += 1;
		} else if (close_mouth == true && ticks >= 5) {
			ticks += 1;
            //This can be changed later.  I just like the sudden drop
			transform.position = new Vector3 (transform.position.x, transform.position.y - .1f, transform.position.z);
		}
		if (ticks == 20) {
			Destroy (gameObject);
		}
	
	}

    //Only close mouth if player
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			close_mouth = true;
			anim.SetBool ("closeMouth", true);
		}
	}
}
