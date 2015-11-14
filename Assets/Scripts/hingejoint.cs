using UnityEngine;
using System.Collections;

public class hingejoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HingeJoint hinge = GetComponent<HingeJoint>();
		JointMotor motor = hinge.motor;
		motor.force = 100;
		motor.targetVelocity = 90;
		motor.freeSpin = false;
		hinge.motor = motor;
		hinge.useMotor = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
