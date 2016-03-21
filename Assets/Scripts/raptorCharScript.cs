using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class raptorCharScript : MonoBehaviour
{
    public float maxSpeed = .3f;
    int facingRight = 1;
    bool coolDown = false;

    Animator anim;

    float coolDownTicks = 5;

    bool grounded = false;
    bool jumpedOnEnemy = false;
    bool jumpedOnFalling = false;
    int ticks = 0;
    public Transform groundCheck;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;
    public float jumpForce = .5f;
    public bool isPunching = false;
    public float egg_speed = 10;
    
    public Rigidbody2D clawShockwave;
    public GameObject claw_obj;
    playerProgress otherMethods;
    healthMethods health;
    GameObject fist;

    public LayerMask whatIsEnemy;
    
    //Access hp bar and animator
    // Use this for initialization
    void Start()
    {
        health = gameObject.GetComponent<healthMethods>();
        otherMethods = gameObject.GetComponent<playerProgress>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check for ground, etc
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);



        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && facingRight == -1)
        {
            Flip();
        }
        else if (move < 0 && facingRight == 1)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerProgress.hasRaptorForm = true;
            Vector3 position = this.GetComponent<Transform>().position;
            Quaternion rotation = this.GetComponent<Transform>().rotation;
            string form;
            if (playerProgress.hasTriceraForm)
            {
                form = "tricera";
                otherMethods.switchForms(position, rotation, form, health.hp);
            }
            else if (playerProgress.hasTRexForm)
            {
                form = "tRex";
                otherMethods.switchForms(position, rotation, form, health.hp);
            }
            else
            {
                form = "egg";
                otherMethods.switchForms(position, rotation, form, health.hp);

            }
        }
        //Start punching upon getting the Enter key
        if (Input.GetKey(KeyCode.Return) && ticks == 0)
        {
            ticks += 1;
            isPunching = true;
            anim.SetBool("isPunching", true);
        }
        if (ticks > 0)
        {
            ticks += 1;
            //If at certain stage in animation create a fist object
            if (ticks == 4 && isPunching)
            {
                if (facingRight == 1) { fist = Instantiate(claw_obj, transform.position + (transform.right * facingRight * .13f) + (transform.up * -.10f), Quaternion.Euler(0, 0, 0)) as GameObject; }
                else { fist = Instantiate(claw_obj, transform.position + (transform.right * facingRight * .13f) + (transform.up * -.10f), Quaternion.Euler(0, 180, 0)) as GameObject; }
                fist.transform.parent = transform;
            }
            //If ticks is at the end of the animation, and the enter key isn't pressed, destroy fist and carry on
            if (ticks >= 9 && !Input.GetKey(KeyCode.Return) && isPunching == true)
            {
                Destroy(fist);
                ticks = 0;
                isPunching = false;
                anim.SetBool("isCharging", false);
                anim.SetBool("isPunching", false);
            }
            //If charged enough, and enter is released
            else if (ticks >= 100 && !Input.GetKey(KeyCode.Return))
            {
                anim.SetBool("isCharging", false);
                Rigidbody2D shot_beam;
                if (facingRight == 1)
                {
                    shot_beam = Instantiate(clawShockwave, new Vector3(transform.position.x + .1f + GetComponent<Rigidbody2D>().velocity.x / 10, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0)) as Rigidbody2D;
                    shot_beam.velocity = new Vector2(egg_speed, 0);
                }
                else {
                    shot_beam = Instantiate(clawShockwave, new Vector3(transform.position.x - .1f + GetComponent<Rigidbody2D>().velocity.x / 10, transform.position.y, transform.position.z), Quaternion.Euler(0, 180, 0)) as Rigidbody2D;
                    shot_beam.velocity = new Vector2(-egg_speed, 0);
                }

                ticks = 0;
                isPunching = false;
                anim.SetBool("isPunching", false);
            }
            //If you stopped charging before the time allowed
            else if (ticks == 9 && Input.GetKey(KeyCode.Return))
            {
                Destroy(fist);
                anim.SetBool("isPunching", false);
            }
            //If you stopped charging before the time allowed
            else if (ticks > 100 && Input.GetKey(KeyCode.Return))
            {
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

    void Update()
    {

        anim.SetBool("flipped", false);

        if ((grounded == true || jumpedOnFalling == true) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            jumpedOnFalling = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
        if (gameObject.transform.position.y <= -100)
        {
            otherMethods.bringBackToLife(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Ignore if it is your own projectile
        if (col.gameObject.tag == "projectile" && !col.gameObject.name.Contains("enemy"))
        {
            return;
        }

        //Ignore damage if button
        if (col.gameObject.name.Contains("button") || col.gameObject.tag == "environment")
        {
            return;
        }
        if (col.gameObject.name.Contains("lavaHazard"))
        {
            health.damagePlayer();
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 100));
        }

        //Check if you jumped on the enemy
        jumpedOnEnemy = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsEnemy);
        //Damage player on any contact with a boss
        if (col.gameObject.tag == "boss")
        {
            health.damagePlayer();
        }
        if (jumpedOnEnemy == true && !col.gameObject.name.Contains("tricera_truck") && !col.gameObject.name.Contains("Proto")
            && !col.gameObject.name.Contains("ankylo") && !col.gameObject.name.Contains("flameShot") && !col.gameObject.name.Contains("fireStego")
            && !col.gameObject.name.Contains("wideFlames") && coolDown == false)
        {
            if ((col.gameObject.name.Contains("parasaurolophus") && col.gameObject.GetComponent<paraScript>().electric) ||
                col.gameObject.name.Contains("electraProto"))
            {
                health.damagePlayer();
            }
            else {
                if (col.gameObject.name.Contains("lavaSnail"))
                {
                    jumpedOnFalling = true;
                    return;
                }
                Destroy(col.gameObject);
                anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                jumpedOnEnemy = false;
            }
            coolDown = true;
        }
        //Don't damage enemy if it is a head trap and also allow you to jump once more
        else if (col.gameObject.name.Contains("apato_head_trap"))
        {
            jumpedOnFalling = true;
            return;
        }
        else if ((col.gameObject.tag == "enemy" || col.gameObject.tag == "truck") && !isPunching && coolDown == false)
        {
            health.damagePlayer();
            coolDown = true;
        }
    }

    void Flip()
    {
        anim.SetBool("flipped", true);
        facingRight = -1 * facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
