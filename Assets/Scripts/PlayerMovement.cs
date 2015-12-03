using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
	
{

	public GameObject topCharacter;
	public GameObject bottomCharacter;
	public int playerNumber;
	public Transform opponent;

	Vector3 movementVector;
	CharacterController characterController;
	float movementSpeed = 32; // 8
	float jumpPower = 15;
	float gravity = 40;

	public Animator topPlayerAnimator;
	public Animator bottomPlayerAnimator;

	public float lastTrigger;

	//bool playAnim;
	List<KeyCode> playerKeys;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		topPlayerAnimator = topCharacter.GetComponent<Animator>();
		bottomPlayerAnimator = bottomCharacter.GetComponent<Animator>();

		//playAnim = false;
		if (playerNumber == 1) {
			playerKeys = new List<KeyCode> (new KeyCode[] {
				KeyCode.W,
				KeyCode.A,
				KeyCode.S,
				KeyCode.D,
				KeyCode.T,
				KeyCode.Y
			});
		} else {
			playerKeys = new List<KeyCode> (new KeyCode[] {
				KeyCode.UpArrow,
				KeyCode.LeftArrow,
				KeyCode.DownArrow,
				KeyCode.RightArrow,
				KeyCode.Keypad7,
				KeyCode.Keypad8
			});
		}
	}
		
	void Update()
	{
		movementVector = new Vector3 (0f, movementVector.y, 0f);

		if ((Input.GetKey (playerKeys[0])) || 
		    (Input.GetKey (playerKeys[1])) ||
		    (Input.GetKey (playerKeys[2])) ||
		    (Input.GetKey (playerKeys[3])) ) {
			// StartCoroutine("Walk");

		    bottomPlayerAnimator.SetBool("IsWalking", true);
		} else {
			bottomPlayerAnimator.SetBool("IsWalking", false);
		}

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
		//if (Input.GetKey (playerKeys[4])) {
		//	transform.Rotate(0f, 10f, 0f);
		//}
		//if (Input.GetKey (playerKeys[5])) {
		//	transform.Rotate(0f, -10f, 0f);
		//}

		//Jump Logic CHECK IF GROUNDED, IF NOT APPLY JUMP POWER
		if(characterController.isGrounded)
		{
			movementVector.y = 0;
			if(Input.GetKeyDown (playerKeys[4]))
			{
				movementVector.y = jumpPower;
			}
			
		}

		//Attack Logic PLAY ANIMATION TO ATTACK
		if (Input.GetKeyDown(playerKeys[5])) {
			//playAnim = !playAnim;
			//StartCoroutine("Attack");
			topPlayerAnimator.SetTrigger ("AttackTrigger");
			lastTrigger = Time.time;
		} 
		//topPlayerAnimator.SetBool ("IsAttacking", playAnim); 

		//Apply final movement vector
		movementVector.y -= gravity * Time.deltaTime;
		characterController.Move(movementVector * Time.deltaTime);
		transform.LookAt (opponent);

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

	//public IEnumerator Walk () {
		
		//bottomPlayerAnimator.SetBool("IsWalking", true);
		
		// yield return new WaitForSeconds(0.5f);
		
		// bottomPlayerAnimator.SetBool("IsWalking", false);
		
	//}
	
}