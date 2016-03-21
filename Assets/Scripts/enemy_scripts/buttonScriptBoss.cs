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
            //If shot at by player
            if (col.gameObject.name.Contains("egg_shot"))
            {
                Destroy(col.gameObject);
            }
            //If jumped on by player
            if (col.gameObject.name.Contains("Character"))
            {
                //Add hp (for now until this gets fixed) and make player jump a little
                col.gameObject.GetComponent<healthMethods>().hp += 1;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            }
            //Destroy self and subtract health from boss
			//AudioSource audio = GetComponent<AudioSource>();
			//audio.Play();
            Destroy(gameObject);
            if(transform.parent.gameObject.name.Contains("robo_raptor")) {
                transform.parent.gameObject.GetComponent<robo_raptor_boss_script>().hp -= 1;
                if (transform.parent.gameObject.GetComponent<robo_raptor_boss_script>().hp == 0)
                {
                    Debug.Log("ITS KILL!");
                    //Destroy(transform.parent.gameObject);
                }
            } else if (transform.parent.gameObject.name.Contains("triceraBoss"))
            {
                transform.parent.gameObject.GetComponent<triceraBossScript>().hp -= 1;
                transform.parent.gameObject.GetComponent<triceraBossScript>().fallback = true;
                transform.parent.gameObject.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y - .2f, 0);
                if (transform.parent.gameObject.GetComponent<triceraBossScript>().hp == 0)
                {
                    Debug.Log("ITS KILL!");
                    //Destroy(transform.parent.gameObject);
                }

            }
            
        }
    }
}

