using UnityEngine;
using System.Collections;

public class oviraptor_script : MonoBehaviour
{
    bool facingRight = true;
    public GameObject player;
    bool shootEgg = false;
    Animator anim;
    Vector3 walkAmount;

    public Rigidbody2D egg_shot;
    float egg_speed = 1f;

    int ticks = 0;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ticks % 60 == 14 && shootEgg == true)
        {
            Rigidbody2D shot_egg;
            if (facingRight) { shot_egg = Instantiate(egg_shot, new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D; }
            else { shot_egg = Instantiate(egg_shot, new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D; }
            shot_egg.velocity = new Vector2(egg_speed, 0);

        } else if(ticks % 60 >= 14 && shootEgg == true)
        {
            anim.SetBool("YesShoot", false);

        }
        if (player && Mathf.Abs(player.transform.position.x - transform.position.x) < 5.0f)
        {
            if (player.transform.position.x > transform.position.x && !facingRight)
            {
                Flip();
            } else if(player.transform.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            ticks += 1;
            shootEgg = true;
            if(ticks % 60 < 14)
            { anim.SetBool("YesShoot", true);
            }
        } else {
            ticks = 0;
            shootEgg = false;
            anim.SetBool("YesShoot", false);
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