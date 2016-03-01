using UnityEngine;
using System.Collections;

public class paraScript : MonoBehaviour {
	bool facingRight = true;
	Animator anim;
	public bool electric = false;
	public float move = -.75f;
	Vector3 walkAmount;

    int wait = 10;
    bool shouldWait = false;

    int ticks = 0;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {

        if (shouldWait)
        {
            wait -= 1;
            if (wait == 0)
            {
                shouldWait = false;
                wait = 10;
            }
        }
        ticks += 1;
		if (ticks % 192 >= 96) {
			electric = true;
		} else {
			electric = false;
		}
		walkAmount.x = move * Time.deltaTime;
		//Debug.Log (walkAmount.x);
		transform.Translate (walkAmount);

	}

	void OnCollisionEnter2D (Collision2D col) {
		//Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "environment" || col.gameObject.name.Contains("stairs") || col.gameObject.tag == "enemy" ||
            col.gameObject.name.Contains("edge")) {
            if (shouldWait == true)
            {
                return;
            }
            Flip ();
            if (col.gameObject.name.Contains("edge"))
            {
                shouldWait = true;
            }
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
