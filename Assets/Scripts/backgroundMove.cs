using UnityEngine;
using System.Collections;

public class backgroundMove : MonoBehaviour {

    Vector3 moveAmount;
    float move = 20f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        moveAmount.x = -move * Time.deltaTime;
        transform.Translate(moveAmount);
        if(transform.position.x <= -100)
        {
            transform.position = new Vector3(100, transform.position.y, transform.position.z);
        }

    }
}
