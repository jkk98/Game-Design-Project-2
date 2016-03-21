using UnityEngine;
using System.Collections;

public class dilophoScript : MonoBehaviour
{
    bool facingRight = false;
    public float xShoot = .5f;
    public float yShoot = .1f;
    public GameObject player;
    bool breatheFlames = false;
    Animator anim;
    Vector3 walkAmount;

    public GameObject flames;

    int ticks = 0;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (ticks % 100 == 10 && breatheFlames == true)
        {
            //Shoot egg at the specified x and y values if player is nearby
            GameObject breathedFlames;
            if (facingRight)
            {
                breathedFlames = Instantiate(flames, new Vector3(transform.position.x + xShoot, transform.position.y + yShoot, transform.position.z), Quaternion.Euler(0, 0, 180)) as GameObject;
                Destroy(breathedFlames, .5f);
            }
            else {
                breathedFlames = Instantiate(flames, new Vector3(transform.position.x - xShoot, transform.position.y + yShoot, transform.position.z), transform.rotation) as GameObject;
                Destroy(breathedFlames, .5f);
            }


        }
        else if (ticks % 100 == 40 && breatheFlames == true)
        {
            anim.SetBool("flames", false);
            anim.SetBool("roar", true);

        }
        if (player && Mathf.Abs(player.transform.position.x - transform.position.x) < 5.0f)
        {
            if (player.transform.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            ticks += 1;
            breatheFlames = true;
            if (ticks % 100 == 0)
            {
                anim.SetBool("flames", true);
                anim.SetBool("roar", false);
            }
        }
        else {
            ticks = 0;
            breatheFlames = false;
            anim.SetBool("flames", false);
            anim.SetBool("roar", true);
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