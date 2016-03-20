using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController controller;

	// Add vairables as necessary for existing through out the game
	public float max_health = 10;
	public int max_lives = 3;

	public float current_health = 10;
	public int current_lives = 3;

	public int current_scene_index = 1;

	// Makes sure there is a single GameController that is created
	// at the beginning of the program
	void Awake () {
		if (controller == null) {
			Debug.Log ("Initiallizing New Game Controller");
			DontDestroyOnLoad (gameObject);
			controller = this;
		} 
		else if (controller != this) {
			Destroy (gameObject);
			Debug.Log ("Preventing duplicate Game Controller from being created");
		}
	}
	
	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 30), "Health: " + current_health);
		GUI.Label (new Rect (10, 40, 100, 30), "Lives: " + current_lives);
	}

	void FixedUpdate()
	{
		if (current_health <= 0) {
			Debug.Log ("Ran out of Health");
			current_health = max_health;
			current_lives--;
			SceneManager.LoadScene (current_scene_index);
		}
		if (current_lives <= 0) {
			Debug.Log ("Ran out of lives");
		}
	}
}
