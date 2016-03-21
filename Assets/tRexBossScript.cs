using UnityEngine;
using System.Collections;

public class tRexBossScript : MonoBehaviour
{

    public GameObject button;
    public GameObject fireBeam;
    public LayerMask target;
    public int hp = 10;
    public GameObject player;
    bool breatheFire = false;
    bool explode = false;
    bool releaseLava = false;
    int ticks = 0;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!player || player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        //If player is nearby
        if (player && Mathf.Abs(player.transform.position.x - transform.position.x) < 10.0f
                   && Mathf.Abs(player.transform.position.y - transform.position.y) < 10.0f)
        {
            if (hp == 10 && transform.childCount == 0)
            {
                Debug.Log("ACTIVATE");
            }
            //Do kill animation if hp at 0
            if (hp == 0)
            {
                anim.SetBool("walk", false);
                anim.SetBool("fire", false);
                anim.SetBool("explosion", false);
                anim.SetBool("dead", true);
                breatheFire = false;
                explode = false;

                ticks += 1;
            }
            if (hp == 0 && ticks >= 400)
            {
                Destroy(gameObject);
            }
            //If no button
            if (transform.childCount == 0)
            {
                //Breathe fire if hp is greater than five
                if (hp > 5)
                {
                    breatheFire = true;
                    Debug.Log("Breathin Fire!");
                } else if (hp == 5)
                {
                    releaseLava = true;
                    anim.SetBool("releaseLava", true);
                    Debug.Log("I'm LEAKING!");
                } else if (hp < 5 && hp > 0)
                {

                }
                //Alternate between chest and head buttons if not firing laser
                Debug.Log("CREATE BUTTON");
                // Put button on door if hp is greater than 5
                if (hp > 5)
                {
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * -.76f), Quaternion.Euler(0, 0, 270));
                    cur_button.transform.parent = transform;
                }
                if (hp == 5 && releaseLava)
                {
                    ticks += 1;
                    if (ticks == 300)
                    {
                        releaseLava = false;
                        ticks = 0;
                    }
                }
                //Else release on top of head
                else if (hp <= 5 && !releaseLava)
                {
                    explode = true;
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * -.30f) + (transform.up * 1.00f), Quaternion.Euler(0, 0, 0));
                    cur_button.transform.parent = transform;
                }

            }
            if (breatheFire && ticks == 25)
            {
                Debug.Log("Take a Breather");
                breatheFire = false;
                anim.SetBool("fire", false);
                ticks = 0;
            }
            if (explode && ticks < 50)
            {
                ticks += 1;
            }
            //If player is above the raptor than shoot a straight laser, else shoot to the ground
            if (explode && ticks == 25)
            {
                Debug.Log("explode!");
                if (player.transform.position.y <= transform.position.y)
                {
                    GameObject lazor1 = (GameObject)Instantiate(fireBeam, transform.position + (transform.right * -2.50f) + (transform.up * .25f), Quaternion.Euler(0, 0, 30));
                    lazor1.transform.parent = transform;
                    Destroy(lazor1, 0.68333f);
                }
                else
                {
                    GameObject lazor1 = (GameObject)Instantiate(fireBeam, transform.position + (transform.right * -3.90f) + (transform.up * 0.90f), Quaternion.Euler(0, 0, 0));
                    lazor1.transform.parent = transform;
                    Destroy(lazor1, 0.68333f);
                }
            }
            if (explode && ticks == 50)
            {
                anim.SetBool("lazor", true);
                ticks = 0;
            }
            if (!breatheFire && !explode)
            {
                ticks += 1;
            }
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("lazor", false);
            anim.SetBool("slash", false);
        }

    }
}
