using UnityEngine;
using System.Collections;

public class triceraBossScript : MonoBehaviour {
    public GameObject button;
    public Rigidbody2D cannonBall;
    public GameObject carObject;
    public Transform hornArea;
    public LayerMask target;
    public GameObject player;
    public bool fallback = false;
    bool activate = false;
    bool chargedPlayer = false;
    bool dead = false;
    float originalY;
    public int hp = 10;

    Animator anim;

    int ticks = 0;


    public void activateBoss()
    {
        activate = true;
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        originalY = transform.position.y;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (activate)
        {
            if(hp <= 4 && fallback)
            {
                fallback = false;
            }
            if (hp == 0 && !dead)
            {
                Debug.Log("DEAD");
                anim.SetBool("damaged", true);
                anim.SetBool("charge", false);
                anim.SetBool("cannons", false);
                dead = true;
                ticks = 0;
            }
            else if (fallback && carObject.transform.position.x - gameObject.transform.position.x < 10f)
            {
                anim.SetBool("damaged", true);
                anim.SetBool("charge", false);
                anim.SetBool("cannons", false);
                transform.Translate(new Vector3(-2f * Time.deltaTime, 0, 0));
            }
            else
            {
                fallback = false;
                anim.SetBool("damaged", false);
                gameObject.transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
            }
            if(carObject.transform.position.x - gameObject.transform.position.x > 5.45f && !fallback && hp != 0)
            {
                anim.SetBool("damaged", false);
                transform.Translate(new Vector3(.4f * Time.deltaTime, 0, 0));
            } else if (!fallback)
            {
                if (transform.childCount == 1)
                {
                    Debug.Log("CREATE BUTTON");
                    if (hp > 4)
                    {
                        GameObject cur_button;
                        cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * .70f) + (transform.up * 1.2f), Quaternion.Euler(0, 0, 270));
                        cur_button.transform.parent = transform;
                    }
                    else if (hp <= 4 && hp % 2 == 1 && hp > 0)
                    {
                        anim.SetBool("charge", false);
                        GameObject cur_button;
                        cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * .70f) + (transform.up * 1.2f), Quaternion.Euler(0, 0, 270));
                        cur_button.transform.parent = transform;
                    }
                    //Else put button on chest
                    else if (hp <= 4 && hp > 0)
                    {
                        anim.SetBool("charge", false);
                        GameObject cur_button;
                        //cur_button = (GameObject)Instantiate(button, transform.position + (transform.up * 1f), Quaternion.Euler(0, 0, 0));
                        cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * .70f) + (transform.up * 1.2f), Quaternion.Euler(0, 0, 270));
                        cur_button.transform.parent = transform;
                    }
                    Debug.Log(hp);

                }
                ticks += 1;
                if (hp == 0)
                {
                    Debug.Log(ticks);
                    Debug.Log(fallback);
                }
                if (ticks == 34 && hp == 0)
                {
                    anim.SetBool("dead", true);
                }
                if (ticks == 180 && hp == 0)
                {
                    playerProgress.hasTriceraForm = true;
                    Destroy(gameObject);
                }
                if(hp > 4) {
                    if (!chargedPlayer && Physics2D.OverlapCircle(hornArea.position, 1.0f, target))
                    {
                        chargedPlayer = true;
                        player.GetComponent<healthMethods>().damagePlayer();

                    }
                    if (ticks % 25 == 0)
                    {
                        anim.SetBool("charge", false);
                        chargedPlayer = true;
                    }
                    if (ticks % 100 == 0)
                    {
                        anim.SetBool("charge", true);
                        chargedPlayer = false;
                        Debug.Log("CHARGE!");
                    }
                }
            }
            if (hp <= 4 && hp % 2 == 1 && hp > 0)
            {
                if(ticks % 150 == 55)
                {
                    anim.SetBool("cannons", false);
                }
                if (ticks % 150 == 50)
                {
                    Rigidbody2D cannonBall1;
                    Rigidbody2D cannonBall2;
                    Rigidbody2D cannonBall3;
                    cannonBall1 = Instantiate(cannonBall, transform.position + (transform.right * 1.2f) + (transform.up * 0.7f), Quaternion.Euler(0, 0, 270)) as Rigidbody2D;
                    cannonBall1.velocity = new Vector2(4.0f, 0);
                    cannonBall2 = Instantiate(cannonBall, transform.position + (transform.right * 1.2f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 270)) as Rigidbody2D;
                    cannonBall2.velocity = new Vector2(4.0f, 0);
                    cannonBall3 = Instantiate(cannonBall, transform.position + (transform.right * 1.2f) + (transform.up * 1.3f), Quaternion.Euler(0, 0, 270)) as Rigidbody2D;
                    cannonBall3.velocity = new Vector2(4.0f, 0);
                }
                if (ticks % 150 == 0)
                {
                    anim.SetBool("cannons", true);
                }
            }

            if (hp <= 4 && hp % 2 == 0 && hp > 0)
            {
                if (ticks % 150 == 55)
                {
                    anim.SetBool("cannons", false);
                }
                if (ticks % 150 == 50)
                {
                    Rigidbody2D cannonBall1;
                    Rigidbody2D cannonBall2;
                    Rigidbody2D cannonBall3;
                    cannonBall1 = Instantiate(cannonBall, transform.position + (transform.right * 1.2f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 270)) as Rigidbody2D;
                    cannonBall1.velocity = new Vector2(10f, 0);
                    cannonBall1.gravityScale = 1f;
                    cannonBall2 = Instantiate(cannonBall, transform.position + (transform.right * 1.2f) + (transform.up * 1.3f), Quaternion.Euler(0, 0, 270)) as Rigidbody2D;
                    cannonBall2.velocity = new Vector2(7.5f, 0);
                    cannonBall2.gravityScale = 1f;
                    cannonBall3 = Instantiate(cannonBall, transform.position + (transform.right * 1.2f) + (transform.up * 1.6f), Quaternion.Euler(0, 0, 270)) as Rigidbody2D;
                    cannonBall3.velocity = new Vector2(5f, 0);
                    cannonBall3.gravityScale = 1f;
                }
                if (ticks % 150 == 0)
                {
                    anim.SetBool("cannons", true);
                }
            }
        }
	
	}
}
