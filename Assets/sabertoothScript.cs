using UnityEngine;
using System.Collections;

public class sabertoothScript : MonoBehaviour
{
    Animator anim;
    bool facingRight = true;
    public float move = -1.5f;
    Vector3 walkAmount;
    int wait = 10;
    bool shouldWait = false;
    public Transform clawSpot;
    public LayerMask target;
    bool alreadySlashed;
    bool slash = false;
    GameObject player;
    int ticks = 0;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Just keep walking and wait for a while if a collision occurs
        if (shouldWait)
        {
            wait -= 1;
            if (wait == 0)
            {
                shouldWait = false;
                wait = 10;
            }
        }
        if (!player)
        {
            player = GameObject.Find("Character");
        }
        //If player nearby shoot cannons
        if (player && Mathf.Abs(player.transform.position.x - transform.position.x) < 0.8f)
        {
            anim.SetBool("slash", true);
            slash = true;
        }
        else if (slash == false)
        {
            anim.SetBool("slash", false);
            slash = false;
            walkAmount.x = move * Time.deltaTime;
            //Debug.Log (walkAmount.x);
            transform.Translate(walkAmount);
        }
        if (slash == true && ticks == 16)
        {
            Debug.Log("ROAR!");
            alreadySlashed = false;
            anim.SetBool("slash", false);
            slash = false;
            ticks = 0;
        }
        if (slash == true && ticks >= 13 && ticks < 16 && !alreadySlashed)
        {
            ticks += 1;
            bool slashedPlayer = Physics2D.OverlapCircle(clawSpot.position, .1f, target);
            if (slashedPlayer)
            {
                player.gameObject.GetComponent<EggScript>().damagePlayer();
                alreadySlashed = true;
            }
        }
        else if (slash == true)
        {
            ticks += 1;
        }


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Turn around if a collidable object gets in the way unless a collision has just occurred
        if (col.gameObject.tag == "environment" || col.gameObject.name.Contains("stairs") || col.gameObject.tag == "enemy" ||
            col.gameObject.name.Contains("edge") || (gameObject.GetComponent<Rigidbody2D>().gravityScale == 0 && col.gameObject.tag == "platform"))
        {
            if (shouldWait == true)
            {
                return;
            }
            Flip();
            if (col.gameObject.name.Contains("edge"))
            {
                shouldWait = true;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        move *= -1;
        transform.localScale = theScale;
    }
}