using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadHub : MonoBehaviour {

	public int hub_scene_index = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void load_Hub() {
		SceneManager.LoadScene (hub_scene_index);
	}
}
