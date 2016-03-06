using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EggScript : MonoBehaviour {
	public float maxSpeed = .5f;
	int facingRight = 1;
	bool coolDown = false;

	Animator anim;

	float coolDownTicks = 5;

	bool grounded = false;
	bool jumpedOnEnemy = false;
	bool jumpedOnFalling = false;
	int ticks = 0;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = .5f;
	public bool isPunching = false;
	public float egg_speed = 2400;
	public int hp = 10;
	public Rigidbody2D egg_shot;
    public GameObject fist_obj;
    GameObject fist;

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

		if (move > 0 && facingRight == -1) {
			Flip ();
		} else if (move < 0 && facingRight == 1) {
			Flip ();
		}
        if (Input.GetKey(KeyCode.Return) && ticks == 0)
        {
            ticks += 1;
            isPunching = true;
            anim.SetBool("isPunching", true);
        }
        if (ticks > 0)
        {
            ticks += 1;
            if (ticks == 5 && isPunching)
            {
                if(facingRight == 1) { fist = Instantiate(fist_obj, transform.position + (transform.right * facingRight * 0.26f) + (transform.up * -.03f), Quaternion.Euler(0, 0, 0)) as GameObject; }
                else { fist = Instantiate(fist_obj, transform.position + (transform.right * facingRight * 0.26f) + (transform.up * -.03f), Quaternion.Euler(0, 180, 0)) as GameObject; }
                fist.transform.parent = transform;
            }
            if (ticks >= 15 && !Input.GetKey(KeyCode.Return) && isPunching == true)
            {
                ticks = 0;
                isPunching = false;
                Destroy(fist);
                anim.SetBool("isCharging", false);
                anim.SetBool("isPunching", false);
            }
            else if (ticks >= 15 && !Input.GetKey(KeyCode.Return))
            {
                anim.SetBool("isCharging", false);
                Rigidbody2D shot_egg;
                if (facingRight == 1)
                {
                    shot_egg = Instantiate(egg_shot, new Vector3(transform.position.x + .01f + GetComponent<Rigidbody2D>().velocity.x / 10, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
                    shot_egg.velocity = new Vector2(egg_speed, 0);
                }
                else {
                    shot_egg = Instantiate(egg_shot, new Vector3(transform.position.x - .01f + GetComponent<Rigidbody2D>().velocity.x / 10, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
                    shot_egg.velocity = new Vector2(-egg_speed, 0);
                }

                ticks = 0;
                isPunching = false;
                anim.SetBool("isPunching", false);
            }
            else if (ticks > 15 && Input.GetKey(KeyCode.Return))
            {
                Destroy(fist);
                anim.SetBool("isCharging", true);
                isPunching = false;
            }
        }

        if (coolDown == true)
        {
            coolDownTicks -= 1;
            if (coolDownTicks == 0)
            {
                coolDown = false;
                coolDownTicks = 5;
            }
        }
    }

	void Update() {

		anim.SetBool ("flipped", false);

		if ((grounded == true || jumpedOnFalling == true) && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			jumpedOnFalling = false;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
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
        if(col.gameObject.tag == "projectile")
        {
            return;
        }

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
		} else if ((col.gameObject.tag == "enemy" || col.gameObject.tag == "truck") && !isPunching && coolDown == false) {
            damagePlayer();
			coolDown = true;
		}
        Physics2D.IgnoreLayerCollision(9, 13, false);
    }

	void Flip() {
		anim.SetBool ("flipped", true);
		facingRight = -1 * facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
