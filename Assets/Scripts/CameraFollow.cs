using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;

	int maxY = 0;

	// Use this for initialization
	void Start () {
	
	}

	float findFloorHeight() {
		float nearestDistanceSqr= Mathf.Infinity;
		GameObject[] taggedGameObjects= GameObject.FindGameObjectsWithTag("floor"); 
		Transform nearestObj= null;

		// loop through each tagged object, remembering nearest one found
		for (int i = 0; i < taggedGameObjects.Length; i++) {

			GameObject obj = taggedGameObjects [i];
			Vector3 objectPos = obj.transform.position;
			float distanceSqr = (objectPos - new Vector3(transform.position.x,objectPos.y,transform.position.z)).sqrMagnitude;

			if (distanceSqr < nearestDistanceSqr) {
				nearestObj = obj.transform;
				nearestDistanceSqr = distanceSqr;
			}
		}
		return nearestObj.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float floorHeight = findFloorHeight ();
		if (player) {
			if (player.transform.position.y > floorHeight) {
				this.transform.position = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y + .765f, offset.z);
			} else {
				this.transform.position = new Vector3 (player.transform.position.x + offset.x, 0 + offset.y, offset.z);
			}
		}
	
	}
}
