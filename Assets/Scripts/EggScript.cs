using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EggScript : MonoBehaviour {
	public float maxSpeed = .5f;
	bool facingRight = true;
	bool coolDown = false;

	Animator anim;

	float coolDownTicks = 5;

	bool grounded = false;
	bool jumpedOnEnemy = false;
	bool jumpedOnFalling = false;
	int punchStage = 0;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = .5f;
	public bool isPunching = false;
	public float egg_speed = 2400;
	public int hp = 10;
	public Rigidbody2D egg_shot;

	public LayerMask whatIsEnemy;
	UnityEngine.UI.Slider hpSlider;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		hpSlider = GameObject.Find ("hp_slider").GetComponent<Slider>();
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

		if (coolDown == true) {
			coolDownTicks -= 1;
			if (coolDownTicks == 0) {
				coolDown = false;
				coolDownTicks = 5;
			}
		}

		anim.SetBool ("flipped", false);

		if (punchStage > 0) {
			punchStage += 1;
			if (punchStage >= 8 && !Input.GetKey (KeyCode.Return) && isPunching == true) {
				punchStage = 0;
				isPunching = false;
				anim.SetBool ("isCharging", false);
				anim.SetBool ("isPunching", false);
			} else if (punchStage >= 30 && !Input.GetKey (KeyCode.Return)) {
				anim.SetBool ("isCharging", false);
				Rigidbody2D shot_egg;
				if (facingRight) {
					shot_egg = Instantiate (egg_shot, new Vector3(transform.position.x+.01f+GetComponent<Rigidbody2D>().velocity.x/10,transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
					shot_egg.velocity = new Vector2 (egg_speed, 0);
				} else {
					shot_egg = Instantiate (egg_shot, new Vector3(transform.position.x-.01f+GetComponent<Rigidbody2D>().velocity.x/10,transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
					shot_egg.velocity = new Vector2 (-egg_speed, 0);
				}

				punchStage = 0;
				isPunching = false;
				anim.SetBool ("isPunching", false);
			} else if (punchStage > 8 && Input.GetKey (KeyCode.Return)) {
				anim.SetBool ("isCharging", true);
				isPunching = false;
			}
		}
		if ((grounded == true || jumpedOnFalling == true) && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			jumpedOnFalling = false;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
		if (Input.GetKey (KeyCode.Return)  && punchStage == 0) {
			punchStage += 1;
			isPunching = true;
			anim.SetBool ("isPunching", true);
		}
		if (gameObject.transform.position.y <= -500) {
			Destroy (gameObject);
		}
	}

    public void damagePlayer()
    {
        hp -= 1;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 300));
        hpSlider.value = hp;
        if (hp == 0)
        {
            Destroy(gameObject);
        }

    }

	void OnCollisionEnter2D (Collision2D col) {

		jumpedOnEnemy = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsEnemy);
        if(col.gameObject.tag == "boss")
        {
            damagePlayer();
        }
		if (jumpedOnEnemy == true && !col.gameObject.name.Contains("tricera_truck") && coolDown == false) {
            if (col.gameObject.name.Contains("parasaurolophus") && col.gameObject.GetComponent<paraScript> ().electric) {
                damagePlayer();
			} else {
                Destroy(col.gameObject);
                anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                jumpedOnEnemy = false;
            }
			coolDown = true;
		}
		else if (col.gameObject.name.Contains( "apato_head_trap")) {
			jumpedOnFalling = true;
			return;
		}
		else if ((col.gameObject.tag == "enemy") && isPunching && coolDown == false) {
			if (col.gameObject.name.Contains ("parasaurolophus") && col.gameObject.GetComponent<paraScript> ().electric) {
                damagePlayer();
			} else {
				Destroy (col.gameObject);
			}
			coolDown = true;
		} else if ((col.gameObject.tag == "enemy" || col.gameObject.tag == "truck") && !isPunching && coolDown == false) {
            damagePlayer();
			coolDown = true;
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
