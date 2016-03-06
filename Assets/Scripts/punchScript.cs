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
        Debug.Log(gameObject.name);
        if (col.gameObject.tag == "enemy")
        {
            Physics2D.IgnoreLayerCollision(9, 13, true);
            Debug.Log("PONCH!");
            Destroy(col.gameObject);
        }
        else
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
