using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	public int hub_scene_index = 1;


	// reset everything for a new game here
	void Start () {
		playerProgress.hasRaptorForm = false;
		playerProgress.hasTRexForm = false;
		playerProgress.hasTriceraForm = false;
		playerProgress.lives = 3;
	}

	// Restarts the game at the hub screne
	public void Restart(){
		Debug.Log ("Restarting game");
		SceneManager.LoadScene (hub_scene_index);
	}
		

	// Update is called once per frame
	void Update () {
	
	}
}
