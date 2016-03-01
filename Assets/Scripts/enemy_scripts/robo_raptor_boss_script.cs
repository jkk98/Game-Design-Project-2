using UnityEngine;
using System.Collections;

public class robo_raptor_boss_script : MonoBehaviour {

    public GameObject button;
    public GameObject lazorbeam;
    GameObject lazor1;
    GameObject lazor2;
    GameObject lazor3;
    public int hp = 10;
    bool roar = false;
    public float move = 1.5f;
    public GameObject player;
    bool facingRight = false;
    bool fireLazor = false;
    bool slash = false;
    bool walk = false;
    int ticks = 0;
    float distance = 0;

    Vector3 frontPosition;
    Vector3 topPosition;
    Quaternion frontRotation;
    Quaternion topRotation;

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (player && Mathf.Abs(player.transform.position.x - transform.position.x) < 5.0f
                   && Mathf.Abs(player.transform.position.y - transform.position.y) < 15.0f)
        {
            if (hp == 10 && transform.childCount == 0)
            {
                Debug.Log("ACTIVATE");
                roar = true;
            }
            if(transform.childCount == 0)
            {
                if (hp >= 10)
                {
                    anim.SetBool("walk", true);
                    walk = true;
                } else if (hp < 10)
                {
                    anim.SetBool("lazor", true);
                    walk = false;
                    fireLazor = true;
                    anim.SetBool("walk", false);
                    Debug.Log("I'MMA FIRING MA LAZOR");
                }
                Debug.Log("CREATE BUTTON");
                if(hp == 10)
                {
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * -1f), Quaternion.Euler(0, 0, 90));
                    cur_button.transform.parent = transform;
                    frontPosition = cur_button.transform.position;
                    frontRotation = cur_button.transform.rotation;
                }
                else if (hp == 9)
                {
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.up * 1f), Quaternion.Euler(0, 0, 0));
                    cur_button.transform.parent = transform;
                    topPosition = cur_button.transform.position;
                    topRotation = cur_button.transform.rotation;
                }

            }
            if (roar)
            {

            }
            if (walk)
            {
                //Debug.Log (walkAmount.x);
                transform.Translate(new Vector3(move* Time.deltaTime, 0,0));

            } if (walk != true && fireLazor != true && Mathf.Abs(player.transform.position.x - transform.position.x) < 2.5f)
            {
                //Debug.Log("SLASH");
                //Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x));
                slash = true;
                anim.SetBool("slash", true);
            }
            if (slash == true && ticks < 35)
            {
                ticks += 1;
            }
            if(slash && ticks == 35)
            {
                Debug.Log("STOP SLASHING");
                slash = false;
                anim.SetBool("slash", false);
                ticks = 0;
            }
            if (fireLazor && ticks < 71)
            {
                Debug.Log(ticks);
                ticks += 1;
            }
            if (fireLazor && ticks == 24)
            {
                Debug.Log("FIRIN!");
                GameObject lazor1 = (GameObject)Instantiate(lazorbeam, transform.position + (transform.right * 2.62f) + (transform.up * .75f), Quaternion.Euler(0, 0, 20));
                lazor1.transform.parent = transform;
                Destroy(lazor1, 0.68333f);
                //lazor2 = (GameObject)Instantiate(lazorbeam, transform.position + (transform.up * 1f) + (transform.right * -1f), Quaternion.Euler(0, 0, 0));
                //lazor3 = (GameObject)Instantiate(lazorbeam, transform.position + (transform.up * 1f) + (transform.right * -1f), Quaternion.Euler(0, 0, 0));
            }
            if (fireLazor && ticks == 71)
            {
                ticks = 0;
                //Destroy(lazor2);
                //Destroy(lazor3);
            }
            if (player.transform.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
        }

    }
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
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
