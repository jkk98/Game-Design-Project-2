using UnityEngine;
using System.Collections;

public class clawShockwaveScript : MonoBehaviour {

    int timeAlive;

    // Use this for initialization
    void Start()
    {
        timeAlive = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += 1;
        if (timeAlive == 360)
        {
            Destroy(this.gameObject);
        }

    }

    //Pretty self explanatory
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy" && !col.gameObject.name.Contains("ankylo"))
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("environment") || col.gameObject.CompareTag("floor") || col.gameObject.CompareTag("boss"))
        {
            Destroy(gameObject);
        }
    }
}
