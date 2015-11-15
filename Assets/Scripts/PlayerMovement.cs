﻿using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
	
{
	private Vector3 movementVector;
	private CharacterController characterController;
	private float movementSpeed = 8;
	private float jumpPower = 15;
	private float gravity = 40;
	
	
	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}
	
	
	void Update()
	{
		Debug.Log (Input.GetAxis ("LeftJoystickX"));
		Debug.Log (Input.GetAxis ("LeftJoystickY"));

		/* This doesn't work as well as I hoped
		if (Input.GetAxis ("LeftJoystickX") > 0) {
			movementVector += -transform.right * Input.GetAxis ("LeftJoystickX") * movementSpeed * -1f;
		} else if (Input.GetAxis ("LeftJoystickX") < 0) {
			movementVector += transform.right * Input.GetAxis ("LeftJoystickX") * movementSpeed;
		} else if (Input.GetAxis ("LeftJoystickY") > 0) {
			movementVector += -transform.forward * Input.GetAxis ("LeftJoystickY") * movementSpeed;
		} else if (Input.GetAxis ("LeftJoystickY") < 0) {
			movementVector += transform.forward * Input.GetAxis ("LeftJoystickY") * movementSpeed * -1f;
		}
		*/

		movementVector.x = Input.GetAxis("LeftJoystickX") * movementSpeed;
		movementVector.z = Input.GetAxis("LeftJoystickY") * movementSpeed * -1f;
		
		if(characterController.isGrounded)
		{
			movementVector.y = 0;
			
			if(Input.GetButtonDown("A"))
			{
				movementVector.y = jumpPower;
			}
		}
		
		movementVector.y -= gravity * Time.deltaTime;
		characterController.Move(movementVector * Time.deltaTime);

		if (Input.GetButton ("LeftBumper")) {
			transform.Rotate(0f, 10f, 0f);
		}
		if (Input.GetButton ("RightBumper")) {
			transform.Rotate(0f, -10f, 0f);
		}
		
	}
	
}