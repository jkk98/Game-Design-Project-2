using UnityEngine;
using System.Collections;

public class lakituScript : MonoBehaviour {

	public GameObject player;
	bool facingRight = true;
	public float move = -.75f;
	Vector3 walkAmount;

	public Rigidbody2D egg_bomb;
	float egg_speed = .75f;

	int ticks = 0;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		ticks += 1;
		if (ticks % 156 == 84) {
			Rigidbody2D shot_egg;
			shot_egg = Instantiate (egg_bomb, new Vector3(transform.position.x+.2f,transform.position.y-.1f, transform.position.z), transform.rotation) as Rigidbody2D;
			shot_egg.velocity = new Vector2 (0, egg_speed);
			
		}
		if ( player && player.transform.position.x == transform.position.x) {
			Rigidbody2D shot_egg;
			shot_egg = Instantiate (egg_bomb, new Vector3(transform.position.x+.2f,transform.position.y-.1f, transform.position.z), transform.rotation) as Rigidbody2D;
			shot_egg.velocity = new Vector2 (0, egg_speed);
		}
		walkAmount.x = move * Time.deltaTime;
		//Debug.Log (walkAmount.x);
		transform.Translate (walkAmount);

	}

	void OnCollisionEnter2D (Collision2D col) {
		//Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "environment" || col.gameObject.name.Contains("stairs") || col.gameObject.tag == "enemy" ||
            col.gameObject.name.Contains("edge")) {
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