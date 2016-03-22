using UnityEngine;
using System.Collections;

public class tRexBossScript : MonoBehaviour
{

    public GameObject button;
    public GameObject fireBeam;
    public GameObject lava;
    public LayerMask target;
    public int hp = 10;
    public GameObject player;
    bool breatheFire = false;
    bool explode = false;
    bool releaseLava = false;
    bool lavaReleased = false;
    bool dead = false;
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
            if (hp == 0 && !dead)
            {
                anim.SetBool("fire", false);
                anim.SetBool("explosion", false);
                anim.SetBool("releaseLava", false);
                breatheFire = false;
                explode = false;
                dead = true;

                ticks = 0;
            } else if (hp == 0)
            {
                ticks += 1;
            }
            if (hp == 0 && ticks >= 100)
            {
                anim.SetBool("death", true);
            }
            if (hp == 0 && ticks >= 170)
            {
                playerProgress.hasTRexForm = true;
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
                } else if (hp == 5 && !lavaReleased)
                {
                    ticks = 0;
                    releaseLava = true;
                    breatheFire = false;
                    anim.SetBool("releaseLava", true);
                    //Debug.Log("I'm LEAKING!");
                } else if (hp < 5 && hp > 0)
                {
                    explode = true;
                    breatheFire = false;
                    Debug.Log("Rexplosion!");

                }
                //Alternate between chest and head buttons if not firing laser
                //Debug.Log("CREATE BUTTON");
                // Put button on door if hp is greater than 5
                if (hp > 5)
                {
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.up * -.55f) + (transform.right * -1.35f), Quaternion.Euler(0, 0, 90));
                    cur_button.transform.parent = transform;
                }
                if (hp == 5 && releaseLava)
                {
                    ticks += 1;
                    lavaReleased = true;
                    Debug.Log(ticks);
                    if (ticks == 100)
                    {
                        GameObject lava1 = Instantiate(lava, transform.position + (transform.up * -2.28f) + (transform.right * -1.5f), transform.rotation) as GameObject;
                        lava1.GetComponent<SpriteRenderer>().sortingOrder++;
                    }
                    if (ticks == 120)
                    {
                        GameObject lava1 = Instantiate(lava, transform.position + (transform.up * -2.28f) + (transform.right * -2.5f), transform.rotation) as GameObject;
                        lava1.GetComponent<SpriteRenderer>().sortingOrder++;
                    }
                    if (ticks == 140)
                    {
                        GameObject lava1 = Instantiate(lava, transform.position + (transform.up * -2.28f) + (transform.right * -3.5f), transform.rotation) as GameObject;
                        lava1.GetComponent<SpriteRenderer>().sortingOrder++;
                    }
                    if (ticks == 160)
                    {
                        GameObject lava1 = Instantiate(lava, transform.position + (transform.up * -2.28f) + (transform.right * -4.5f), transform.rotation) as GameObject;
                        lava1.GetComponent<SpriteRenderer>().sortingOrder++;
                    }
                    if (ticks == 180)
                    {
                        GameObject lava1 = Instantiate(lava, transform.position + (transform.up * -2.28f) + (transform.right * -5.5f), transform.rotation) as GameObject;
                        lava1.GetComponent<SpriteRenderer>().sortingOrder++;
                    }
                    if (ticks == 200)
                    {
                        GameObject lava1 = Instantiate(lava, transform.position + (transform.up * -2.28f) + (transform.right * -6.5f), transform.rotation) as GameObject;
                        lava1.GetComponent<SpriteRenderer>().sortingOrder++;
                        GameObject lava2 = Instantiate(lava, transform.position + (transform.up * -2.28f) + (transform.right * -7.5f), transform.rotation) as GameObject;
                        lava2.GetComponent<SpriteRenderer>().sortingOrder++;
                        Debug.Log("Stop leaking");
                        releaseLava = false;
                        ticks = 0;
                    }
                }
                //Else release on top of head
                else if (hp <= 5 && hp > 0 && !releaseLava)
                {
                    explode = true;
                    GameObject cur_button;
                    cur_button = (GameObject)Instantiate(button, transform.position + (transform.right * -.80f) + (transform.up * 2.5f), Quaternion.Euler(0, 0, 0));
                    cur_button.transform.parent = transform;
                }

            }
            if (breatheFire && ticks < 81)
            {
                ticks += 1;
            }
            if (breatheFire && ticks == 61)
            {
                anim.SetBool("fire", false);
            }
            else if (breatheFire && ticks == 81)
            {
                ticks = 0;
            }
            else if (breatheFire && ticks == 1)
            {
                anim.SetBool("fire", true);
            }
            else if (breatheFire && ticks == 36)
            {
                GameObject flameBeam = (GameObject)Instantiate(fireBeam, transform.position + (transform.right * -2.50f) + (transform.up * .15f), Quaternion.Euler(0, 0, 15));
                flameBeam.transform.parent = transform;
                flameBeam.transform.localScale += new Vector3(.3f, 0f, 0f);
                Destroy(flameBeam, .6f);
            }
            if (explode && ticks < 150)
            {
                ticks += 1;
            }
            if (explode && ticks == 1)
            {
                anim.SetBool("explosion", true);
            }
            //If player is above the raptor than shoot a straight laser, else shoot to the ground
            if (explode && ticks == 10)
            {
                Debug.Log("explode!");
                GameObject firePillar = (GameObject)Instantiate(fireBeam, transform.position + (transform.right * -0.50f) + (transform.up * -1f), Quaternion.Euler(0, 0, 90));
                firePillar.transform.parent = transform;
                firePillar.transform.localScale += new Vector3(2f, 6f, 0f);
                Destroy(firePillar, 0.68333f);
            }
            if (explode && ticks == 40)
            {
                anim.SetBool("explosion", false);
            }
            if (explode && ticks == 150)
            {
                ticks = 0;
            }
            if (!breatheFire && !explode && !releaseLava)
            {
                ticks += 1;
            }
        }
        else
        {
            anim.SetBool("fire", false);
            anim.SetBool("explosion", false);
            anim.SetBool("releaseLava", false);
        }

    }
}
