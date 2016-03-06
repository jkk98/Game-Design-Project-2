using UnityEngine;
using System.Collections;

public class DinoScript : MonoBehaviour {
	bool facingRight = true;
	public float move = -.75f;
	Vector3 walkAmount;
    int wait = 10;
    bool shouldWait = false;


	// Use this for initialization
	void Start () {
	
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
		walkAmount.x = move * Time.deltaTime;
		//Debug.Log (walkAmount.x);
		transform.Translate (walkAmount);
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "environment" || col.gameObject.name.Contains("stairs") || col.gameObject.tag == "enemy" ||
            col.gameObject.name.Contains("edge") || (gameObject.GetComponent<Rigidbody2D>().gravityScale == 0 && col.gameObject.tag == "platform")) {
            if(shouldWait == true)
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
