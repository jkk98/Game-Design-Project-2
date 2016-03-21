using UnityEngine;
using System.Collections;

public class archaeoScript : MonoBehaviour
{
    bool facingRight = true;
    public float move = -.75f;
    Vector3 walkAmount;
    int wait = 10;
    bool shouldWait = false;

    float angle = 0;
    float speed = (10 * Mathf.PI) / 5;
    float radius = .5f;
    float originalX;
    float originalY;


    // Use this for initialization
    void Start()
    {
        originalX = transform.position.x;
        originalY = transform.position.y;

    }

    // Update is called once per frame
    void Update()
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
        angle += speed * Time.deltaTime;
        transform.position = new Vector3 (Mathf.Cos (angle) * radius + originalX, Mathf.Sin(angle) * radius + originalY, 0);

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
