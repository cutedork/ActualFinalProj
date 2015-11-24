﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
	
{

	public GameObject topCharacter;
	public int playerNumber;

	Vector3 movementVector;
	CharacterController characterController;
	float movementSpeed = 32; // 8
	float jumpPower = 15;
	float gravity = 40;

	Animator topPlayerAnimator;
	//bool playAnim;
	List<KeyCode> playerKeys;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		topPlayerAnimator = topCharacter.GetComponent<Animator>();
		//playAnim = false;
		if (playerNumber == 1) {
			playerKeys = new List<KeyCode> (new KeyCode[] {
				KeyCode.W,
				KeyCode.A,
				KeyCode.S,
				KeyCode.D,
				KeyCode.Q,
				KeyCode.E,
				KeyCode.Z,
				KeyCode.X
			});
		} else {
			playerKeys = new List<KeyCode> (new KeyCode[] {
				KeyCode.I,
				KeyCode.J,
				KeyCode.K,
				KeyCode.L,
				KeyCode.U,
				KeyCode.O,
				KeyCode.M,
				KeyCode.Comma
			});
		}
	}
		
	void Update()
	{
		movementVector = new Vector3 (0f, movementVector.y, 0f);

		//Movement direction FORWARD, BACK, LEFT, RIGHT
		if (Input.GetKey (playerKeys[0])) {
			movementVector += transform.forward * movementSpeed;
		}
		if (Input.GetKey (playerKeys[1])) {
			movementVector += -transform.right * movementSpeed;
		} 
		if (Input.GetKey (playerKeys[2])) {
			movementVector += -transform.forward * movementSpeed;
		} 
		if (Input.GetKey (playerKeys[3])) {
			movementVector += transform.right * movementSpeed;
		} 

		//Player rotation CLOCKWISE, COUNTERCLOCKWISE
		if (Input.GetKey (playerKeys[4])) {
			transform.Rotate(0f, 10f, 0f);
		}
		if (Input.GetKey (playerKeys[5])) {
			transform.Rotate(0f, -10f, 0f);
		}

		//Jump Logic CHECK IF GROUNDED, IF NOT APPLY JUMP POWER
		if(characterController.isGrounded)
		{
			movementVector.y = 0;
			if(Input.GetKeyDown (playerKeys[6]))
			{
				movementVector.y = jumpPower;
			}
			
		}

		//Attack Logic PLAY ANIMATION TO ATTACK
		if (Input.GetKeyDown(playerKeys[7])) {
			//playAnim = !playAnim;
			StartCoroutine("Attack");
		} 
		//topPlayerAnimator.SetBool ("IsAttacking", playAnim); 

		//Apply final movement vector
		movementVector.y -= gravity * Time.deltaTime;
		characterController.Move(movementVector * Time.deltaTime);


		// Debug.Log (Input.GetAxis ("LeftJoystickX"));
		// Debug.Log (Input.GetAxis ("LeftJoystickY"));
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
		/*
		movementVector.x = Input.GetAxis("LeftJoystickX") * movementSpeed;
		movementVector.z = Input.GetAxis("LeftJoystickY") * movementSpeed * -1f;

		if(characterController.isGrounded)
		{
			movementVector.y = 0;
			
#if UNITY_STANDALONE_WIN
			if(Input.GetButtonDown("A"))
			{
				movementVector.y = jumpPower;
			}
#elif UNITY_STANDALONE_OSX
			if (Input.GetButtonDown("A_OSX"))
			{
				movementVector.y = jumpPower;
			}
#endif

		}
		
		movementVector.y -= gravity * Time.deltaTime;
		characterController.Move(movementVector * Time.deltaTime);

#if UNITY_STANDALONE_WIN
		if (Input.GetButton ("LeftBumper")) {
			transform.Rotate(0f, 10f, 0f);
		}

#elif UNITY_STANDALONE_OSX
		if (Input.GetButton ("LeftBumper_OSX")) {
			transform.Rotate(0f, 10f, 0f);
		}
#endif



#if UNITY_STANDALONE_WIN

		if (Input.GetButton ("RightBumper")) {
			transform.Rotate(0f, -10f, 0f);
		}

#elif UNITY_STANDALONE_OSX
		if (Input.GetButton ("RightBumper_OSX")) {
			transform.Rotate(0f, -10f, 0f);
		}
#endif
		*/

	}

	public IEnumerator Attack () {
		
		topPlayerAnimator.SetBool("IsAttacking", true);
		
		yield return new WaitForSeconds(0.5f);
		
		topPlayerAnimator.SetBool("IsAttacking", false);
		
	}
	
}