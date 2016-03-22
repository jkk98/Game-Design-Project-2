using UnityEngine;
using System.Collections;

public class robo_raptor_boss_script : MonoBehaviour {

    public GameObject button;
    public GameObject lazorbeam;
    public Transform clawSpot;
    public LayerMask target;
    GameObject lazor1;
    public int hp = 10;
    bool roar = false;
    public float move = 1.5f;
    public GameObject player;
    int facingRight = 1;
    bool fireLazor = false;
    bool slash = false;
    bool alreadySlashed = false;
    bool walk = false;
    int ticks = 0;

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }

        //If player is nearby
        if (player && Mathf.Abs(player.transform.position.x - transform.position.x) < 5.0f
                   && Mathf.Abs(player.transform.position.y - transform.position.y) < 15.0f)
        {
            if (hp == 10 && transform.childCount == 1)
            {
                Debug.Log("ACTIVATE");
                //roar = true;
            }
            //Do kill animation if hp at 0
            if (hp == 0)
            {
                anim.SetBool("walk", false);
                anim.SetBool("lazor", false);
                anim.SetBool("slash", false);
                anim.SetBool("dead", true);
                walk = false;
                fireLazor = false;
                slash = false;
                
                ticks += 1;
            }
            if (hp == 0 && ticks >= 400)
            {
                playerProgress.hasRaptorForm = true;
                Destroy(gameObject);
            }
            //If no button
            if(transform.childCount == 1)
            {
                //Do standard walking and slashing if hp is greater than 3
                if (hp > 3)
                {
                    Debug.Log("ACTIVATE");
                    anim.SetBool("walk", true);
                    walk = true;
                }
                //Do laser if hp is at or under 3
                else if (hp <= 3 && hp > 0)
                {
                    anim.SetBool("lazor", true);
                    walk = false;
                    fireLazor = true;
                    anim.SetBool("walk", false);
                    Debug.Log("I'MMA FIRING MA LAZOR");
                    anim.SetBool("lazor", true);
                    //walk = true;
                }
                //Alternate between chest and head buttons if not firing laser
                Debug.Log("CREATE BUTTON");
                if(hp > 3 && hp % 2 == 0)
                {
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * .76f * facingRight), Quaternion.Euler(0, 0, facingRight * 270));
                    cur_button.transform.parent = transform;
                }
                else if (hp > 3 && hp % 2 == 1)
                {
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * .30f * facingRight) + (transform.up * 1.00f), Quaternion.Euler(0, 0, 0));
                    cur_button.transform.parent = transform;
                }
                //Else put button on chest
                else if (hp <= 3 && hp > 0)
                {
                    GameObject cur_button;
                    //cur_button = (GameObject)Instantiate(button, transform.position + (transform.up * 1f), Quaternion.Euler(0, 0, 0));
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * .72f * facingRight) + (transform.up * -.30f), Quaternion.Euler(0, 0, facingRight * 270));
                    cur_button.transform.parent = transform;
                }

            }
            //Could add roaring if we wanted to
            if (roar)
            {

            }
            //Walk if the conditions call for it
            if (walk)
            {
                //Debug.Log (walkAmount.x);
                anim.SetBool("walk", true);
                transform.Translate(new Vector3(facingRight * move* Time.deltaTime, 0,0));

            }
            //If not doing anything at the moment and player is really close, slash at it
            if (walk != true && fireLazor != true && Mathf.Abs(player.transform.position.x - transform.position.x) < 2.5f && ticks == 0)
            {
                slash = true;
                anim.SetBool("slash", true);
            }
            if (slash == true && ticks < 35)
            {
                ticks += 1;
            }
            //Slash player only once until slashing him again (or else the slash will instantly kill him)
            if (slash && ticks > 15 && !alreadySlashed)
            {
                bool slashedPlayer = Physics2D.OverlapCircle(clawSpot.position, .5f, target);
                if (slashedPlayer)
                {
                    player.gameObject.GetComponent<healthMethods>().damagePlayer();
                    alreadySlashed = true;
                }

            }
            if(slash && ticks == 25)
            {
                Debug.Log("STOP SLASHING");
                slash = false;
                anim.SetBool("slash", false);
                ticks = 0;
                alreadySlashed = false;
            }
            if (fireLazor && ticks < 50)
            {
                ticks += 1;
            }
            //If player is above the raptor than shoot a straight laser, else shoot to the ground
            if (fireLazor && ticks == 25)
            {
                Debug.Log("FIRIN!");
                if(player.transform.position.y <= transform.position.y) {
                    GameObject lazor1 = (GameObject)Instantiate(lazorbeam, transform.position + (transform.right * facingRight * 2.50f) + (transform.up * .25f), Quaternion.Euler(0, 0, 30));
                    lazor1.transform.parent = transform;
                    Destroy(lazor1, 0.68333f);
                } else
                {
                    GameObject lazor1 = (GameObject)Instantiate(lazorbeam, transform.position + (transform.right * facingRight * 3.90f) + (transform.up * 0.90f), Quaternion.Euler(0, 0, 0));
                    lazor1.transform.parent = transform;
                    Destroy(lazor1, 0.68333f);
                }
            }
            if (fireLazor && ticks == 50)
            {
                anim.SetBool("lazor", true);
                ticks = 0;
            }
            if (!walk && !fireLazor && !slash)
            {
                ticks += 1;
            }
            //If the player hasn't approached for a while than go to the other room in an effort to "get" him
            if (!walk && !fireLazor && !slash && hp!= 0 && ticks == 150 && hp != 0)
            {
                Debug.Log("ChasePlayer");
                walk = true;
                anim.SetBool("walk", true);
                ticks = 0;

            }
            if (player.transform.position.x > transform.position.x && facingRight == -1 )
            {
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && facingRight == 1)
            {
                Flip();
            }
        } else
        {
            anim.SetBool("walk", false);
            anim.SetBool("lazor", false);
            anim.SetBool("slash", false);
        }

    }
    //Stop walking upon environment collision
    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag == "environment")
        {
            Debug.Log("STOP WALKING");
            walk = false;
            anim.SetBool("walk", false);
            Flip();
        }
    }

    void Flip()
    {
        facingRight = -1 * facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
