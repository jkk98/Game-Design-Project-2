using UnityEngine;
using System.Collections;

public class buttonScriptBoss : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Character") || col.gameObject.name.Contains("egg_shot"))
        {
            if (col.gameObject.name.Contains("egg_shot"))
            {
                Destroy(col.gameObject);
            }
            if (col.gameObject.name.Contains("Character"))
            {
                col.gameObject.GetComponent<EggScript>().hp += 1;
            }
            Destroy(gameObject);
            transform.parent.gameObject.GetComponent<robo_raptor_boss_script>().hp -= 1;
            if (transform.parent.gameObject.GetComponent<robo_raptor_boss_script>().hp == 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}

