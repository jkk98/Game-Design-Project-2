using UnityEngine;
using System.Collections;
using UnityEditor;


public class HazardCollision_2d : MonoBehaviour {

	public enum type_enum {damage, knockback, death, reset, temp_something_else};

	// possible types such as damage, knockback, death, etc
	public type_enum type;

	public double damage = 0;
	public double force = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log (type);
		switch(type){

		case type_enum.reset:
			if (other.gameObject.CompareTag ("Player")) {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("john_level_scene2");
			}
			break;
		}
	}
}
