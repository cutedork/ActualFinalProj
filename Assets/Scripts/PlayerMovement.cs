using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
	
{

	public GameObject topCharacter;
	
	Vector3 movementVector;
	CharacterController characterController;
	float movementSpeed = 16; // 8
	float jumpPower = 15;
	float gravity = 40;

	Animator topPlayerAnimator;
	bool playAnim;
	
	
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		topPlayerAnimator = topCharacter.GetComponent<Animator>();
		playAnim = false;
	}
	
	
	void Update()
	{
		movementVector = new Vector3 (0f, movementVector.y, 0f);

		//Movement direction
		if (Input.GetKey (KeyCode.W)) {
			movementVector += transform.forward * movementSpeed;
		}
		if (Input.GetKey (KeyCode.S)) {
			movementVector += -transform.forward * movementSpeed;
		} 
		
		if (Input.GetKey (KeyCode.A)) {
			movementVector += -transform.right * movementSpeed;
		} 
		if (Input.GetKey (KeyCode.D)) {
			movementVector += transform.right * movementSpeed;
		} 

		if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate(0f, 10f, 0f);
		}

		if (Input.GetKey (KeyCode.E)) {
			transform.Rotate(0f, -10f, 0f);
		}

		if(characterController.isGrounded)
		{
			movementVector.y = 0;
			if(Input.GetKeyDown (KeyCode.Z))
			{
				movementVector.y = jumpPower;
			}
			
		}

		if (Input.GetKeyDown(KeyCode.X)) {
			playAnim = !playAnim;
		} 
		
		topPlayerAnimator.SetBool ("IsAttacking", playAnim); 

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
	
}