using UnityEngine;
using System.Collections;

public class buttonScriptReg : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //If jumped on by player or shot at, kill parent
        if(col.gameObject.name.Contains("Character") || col.gameObject.name.Contains("egg_shot"))
        {   
            if(col.gameObject.name.Contains("egg_shot"))
            {
                Destroy(col.gameObject);
            }
            if(col.gameObject.name.Contains("Character")) {
                col.gameObject.GetComponent<EggScript>().hp += 1;
            }
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
