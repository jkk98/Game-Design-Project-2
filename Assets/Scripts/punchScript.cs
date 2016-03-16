using UnityEngine;
using System.Collections;

public class punchScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //If punch collides with enemy, kill enemy
        if (col.gameObject.tag == "enemy" && !col.gameObject.name.Contains("truck") && !col.gameObject.name.Contains("ankylo") && !col.gameObject.name.Contains("lavaSnail"))
        {
            Debug.Log("PONCH!");
            Destroy(col.gameObject);
        }
        //If anything else, ignore
        else
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
