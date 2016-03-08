using UnityEngine;
using System.Collections;

public class ankyloScript : MonoBehaviour
{
    Animator anim;
    bool facingRight = true;
    public float move = -.75f;
    Vector3 walkAmount;
    int wait = 10;
    bool shouldWait = false;
    bool shootingCannon = false;
    GameObject player;
    public Rigidbody2D cannonBall;
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
        if (player && Mathf.Abs(player.transform.position.x - transform.position.x) >= 2.0f)
        {
            anim.SetBool("shootCannon", true);
            shootingCannon = true;
        }
        else if (shootingCannon == false) {
            anim.SetBool("shootCannon", false);
            shootingCannon = false;
            walkAmount.x = move * Time.deltaTime;
            //Debug.Log (walkAmount.x);
            transform.Translate(walkAmount);
        }
        if (shootingCannon == true && ticks == 40)
        {
            Debug.Log("FIRE!");
            anim.SetBool("shootCannon", false);
            shootingCannon = false;
            ticks = 0;
            Rigidbody2D shotBall1 = Instantiate(cannonBall, new Vector3(transform.position.x, transform.position.y + .35f, transform.position.z), transform.rotation) as Rigidbody2D ;
            shotBall1.velocity = new Vector2(-1.5f, 0);
            Rigidbody2D shotBall2 = Instantiate(cannonBall, new Vector3(transform.position.x + .50f, transform.position.y + .35f, transform.position.z), transform.rotation) as Rigidbody2D;
            shotBall2.velocity = new Vector2(1.5f, 0);
        }
        if (shootingCannon == true && ticks < 40)
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
