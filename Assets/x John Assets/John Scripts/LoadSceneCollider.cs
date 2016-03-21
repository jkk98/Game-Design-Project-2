using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneCollider : MonoBehaviour {
	public string sceneName;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		Debug.Log ("Collision with scene collider");
		if (col.collider.CompareTag ("Player")) {
			Debug.Log ("Loading scene: " + sceneName);
			SceneManager.LoadScene (sceneName);
		}
	}
}
