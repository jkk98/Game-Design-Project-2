using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}

    //Find nearest floor so that the camera knows what to look at if player isn't grounded
	float findFloorHeight() {
		if (!player) {
			return 0;
		}
		float nearestDistanceSqr= Mathf.Infinity;
		GameObject[] taggedGameObjects= GameObject.FindGameObjectsWithTag("floor"); 
		Transform nearestObj= null;

		// loop through each tagged object, remembering nearest one found
		for (int i = 0; i < taggedGameObjects.Length; i++) {

			GameObject obj = taggedGameObjects [i];
			Vector3 objectPos = obj.transform.position;
			float distanceSqr = (objectPos - new Vector3(transform.position.x,objectPos.y,transform.position.z)).sqrMagnitude;

			if (distanceSqr < nearestDistanceSqr && obj.transform.position.y < player.transform.position.y) {
				nearestObj = obj.transform;
				nearestDistanceSqr = distanceSqr;
			}
		}
		if (nearestObj.transform) {
			return nearestObj.transform.position.y;
		} else {
			return player.transform.position.y;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if(!player)
        {
            player = GameObject.Find("Character");
        }
		float floorHeight = findFloorHeight ();
		if (player) {
			if (player.transform.position.y > floorHeight) {
				this.transform.position = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y +offset.y, offset.z);
			} else {
				this.transform.position = new Vector3 (player.transform.position.x + offset.x, 0 + offset.y, offset.z);
			}
		}
	
	}
}
