using UnityEngine;
using System.Collections;

public class carScript : MonoBehaviour {
    public GameObject electraProto;

    int ticks = 0; //50 ticks per second in FixedUpdate, used for creating dinosaurs
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        /*ticks += 1;
        if (ticks % 200 == 0)
        {
            GameObject proto1 = Instantiate(electraProto, transform.position + (transform.right * 1.0f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto2 = Instantiate(electraProto, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }*/
	
	}
}
