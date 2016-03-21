using UnityEngine;
using System.Collections;

public class mosquitoScript : MonoBehaviour {

    int facingRight = -1;
    Animator anim;
    bool suckedLava = false;
    bool shootingFire = false;
    float step;
    int ticks = 0;
    public float speed = 1.0f;
    public GameObject fireBeam;
    GameObject closestLavaSource;
    GameObject player;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

    }

    GameObject FindClosestLavaSource()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("lava");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

// Update is called once per frame
void FixedUpdate () {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
            return;
        }

        if (player.transform.position.x > transform.position.x && facingRight == -1)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && facingRight == 1)
        {
            Flip();
        }
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 15f)
        {
            if (shootingFire && ticks < 37)
            {
                ticks += 1;
            }
            if (shootingFire && ticks == 20)
            {
                GameObject shotFire = Instantiate(fireBeam, new Vector3(transform.position.x + facingRight * 1.97f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                Destroy(shotFire, .3f);
            }
            if (shootingFire && ticks == 37)
            {
                anim.SetBool("firing", false);
                ticks = 0;
                shootingFire = false;
                suckedLava = false;
            }
            closestLavaSource = FindClosestLavaSource();
            if (!suckedLava && (transform.position.x != closestLavaSource.transform.position.x || transform.position.y != closestLavaSource.transform.position.y + 1f))
            {
                step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(closestLavaSource.transform.position.x, closestLavaSource.transform.position.y + 1f, 0), step);

            }
            if (!suckedLava && transform.position.x == closestLavaSource.transform.position.x && transform.position.y == closestLavaSource.transform.position.y + 1f && ticks < 75)
            {
                anim.SetBool("sucking", true);
                ticks += 1;

            }
            else if (!suckedLava && transform.position.x == closestLavaSource.transform.position.x && transform.position.y == closestLavaSource.transform.position.y + 1f && ticks == 75)
            {
                anim.SetBool("sucking", false);
                ticks = 0;
                suckedLava = true;

            }
            if (suckedLava && player.transform.position.y == transform.position.y && !shootingFire)
            {
                shootingFire = true;
                anim.SetBool("firing", true);
            }
            if (suckedLava && player.transform.position.y != transform.position.y && !shootingFire)
            {
                step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3 (transform.position.x, player.transform.position.y, 0), step);

            }
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
