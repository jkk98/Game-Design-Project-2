using UnityEngine;
using System.Collections;
using UnityEditor;


public class HazardCollision_2d : MonoBehaviour {

	public enum type_enum {damage, knockback, death, reset, temp_something_else};

	// possible types such as damage, knockback, death, etc
	public type_enum type;

	public float damage = 0;
	public float force = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	void OnCollisionEnter2D(Collision2D collision){
		//Debug.Log (type);
		Rigidbody2D other_rigidbody = collision.collider.GetComponent<Rigidbody2D>();

		// Getting the average angle of contact
		Vector2 average_normal = new Vector2 (0, 0); 
		// gets the average of all contact points
		foreach (ContactPoint2D contact_point in collision.contacts) {
			average_normal += contact_point.normal;
		}
		average_normal *= -1; // reverses direction outwards
		average_normal.Normalize ();

		//Vector2 normal = collision.contacts[0].normal * -1; // Gets the angle of the contact from first contact point
		Debug.Log(average_normal);

		switch(type){

		case type_enum.reset:
			if (collision.gameObject.CompareTag ("Player")) {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("john_level_scene2");
			}
			break;

		// knockback perpendicular to object
		case type_enum.knockback:
			// NOTE: Seems to work fine on rigid bodies though seems to come in conflict with character controller in terms 
			// of horizontal movement. FixedUpdate may have something to do with this
			other_rigidbody.AddForce (average_normal * force);
			//other_rigidbody.velocity = average_normal * force; // need to decide if a force or velocity is wanted here

			break;
		}
	}
}
