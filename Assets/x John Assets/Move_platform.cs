using UnityEngine;
using System.Collections;

/* Takes a platform with a Slider Joint 2D and allows for it to move and back and 
 * forth between the upper and lower limits. It is recommended that the initial motor speed
 * is set to a positive number. There must also be a workable difference between the lower and 
 * upper limits. Also make sure there is a high amount of friction on horizontal platforms and 
 * be wary of the repercusions from collisions with other rigid bodies.
*/
public class Move_platform : MonoBehaviour {

	private SliderJoint2D slider;
	private JointLimitState2D goal_limit;
	private JointMotor2D nextMotor;

	void Awake () {
		slider = GetComponent<SliderJoint2D>();
	}

	// Use this for initialization
	void Start () {

		// Starts out by getting the initial goal limit
		switch (slider.limitState) {

			// Sets the default goal limit to the upper limit
			case JointLimitState2D.Inactive:
				goal_limit = JointLimitState2D.UpperLimit;
				break;
			
			case JointLimitState2D.UpperLimit:
				goal_limit = JointLimitState2D.LowerLimit;
				break;

			case JointLimitState2D.LowerLimit:
				goal_limit = JointLimitState2D.UpperLimit;
				break;

			case JointLimitState2D.EqualLimits:
				Debug.Log ("Warning: Moving Platform function will not work when the limits are the same");
				break;

		}
			
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(slider.limitState);
		if (slider.limitState == goal_limit) {
			//Debug.Log ("Reached the current goal limit.");

			// Reverses the motor's direction
			// Cannot directly modify the motor values so just replacing it in the meantime
			nextMotor.motorSpeed = slider.motor.motorSpeed * -1;
			nextMotor.maxMotorTorque = slider.motor.maxMotorTorque;
			slider.motor = nextMotor; 

			// Gets the next goal limit
			switch (slider.limitState) {

				// Checking for the Inactive limit state only as a debugging procedure
				case JointLimitState2D.Inactive:
					Debug.Log ("An error may have occured as limit state should not be inactive.");
					break;

				case JointLimitState2D.UpperLimit:
					goal_limit = JointLimitState2D.LowerLimit;
					break;

				case JointLimitState2D.LowerLimit:
					goal_limit = JointLimitState2D.UpperLimit;
					break;

			}
		}
	}
}
