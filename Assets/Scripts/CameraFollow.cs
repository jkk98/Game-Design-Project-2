using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;

	int maxY = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {
			if (player.transform.position.y > 0) {
				this.transform.position = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y + offset.y, offset.z);
			} else {
				this.transform.position = new Vector3 (player.transform.position.x + offset.x, 0 + offset.y, offset.z);
			}
		}
	
	}
}
