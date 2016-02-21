﻿using UnityEngine;
using System.Collections;

public class EggScript : MonoBehaviour {
	public float maxSpeed = .5f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	bool jumpedOnEnemy = false;
	int punchStage = 0;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = .5f;
	public bool isPunching = false;
	public float egg_speed = 1;
	public GameObject egg_shot;

	public LayerMask whatIsEnemy;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);



		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}

	void Update() {
			

		anim.SetBool ("flipped", false);

		if (punchStage > 0) {
			punchStage += 1;
			if (punchStage >= 12 && !Input.GetKey (KeyCode.Return)) {
				punchStage = 0;
				isPunching = false;
				anim.SetBool ("isCharging", false);
				anim.SetBool ("isPunching", false);
			} else if (punchStage >= 30 && !Input.GetKey (KeyCode.Return) && isPunching == true) {
				Debug.Log ("ASS");
				anim.SetBool ("isCharging", false);
				GameObject shot_egg = (GameObject) Instantiate (egg_shot, transform.position, transform.rotation);
				punchStage = 0;
				isPunching = false;
			} else if (punchStage > 12 && Input.GetKey (KeyCode.Return)) {
				anim.SetBool ("isCharging", true);
			}
		}
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
		if (Input.GetKey (KeyCode.Return)  && punchStage == 0) {
			punchStage += 1;
			isPunching = true;
			anim.SetBool ("isPunching", true);
		}
	}

	void OnCollisionEnter2D (Collision2D col) {

		jumpedOnEnemy = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsEnemy);
		if (jumpedOnEnemy == true) {
			Debug.Log ("STOMP");
			Destroy (col.gameObject);
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 800));
			jumpedOnEnemy = false;
		}
		else if (col.gameObject.tag == "enemy" && isPunching) {
			Destroy (col.gameObject);
		} else if (col.gameObject.tag == "enemy" && !isPunching) {
			Destroy (gameObject);
		}
	}

	void Flip() {
		anim.SetBool ("flipped", true);
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
