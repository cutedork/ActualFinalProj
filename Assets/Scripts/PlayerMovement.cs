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

	float FButtonCooler = 0.5f ; // Half a second before reset
	float FButtonCount = 0f;
	float BButtonCooler = 0.5f ; // Half a second before reset
	float BButtonCount = 0f;
	float LButtonCooler = 0.5f ; // Half a second before reset
	float LButtonCount = 0f;
	float RButtonCooler = 0.5f ; // Half a second before reset
	float RButtonCount = 0f;

	float maxDashTime = 0.5f;
	float dashSpeed = 150.0f;
	float dashStoppingSpeed = 0.1f;
	float currentDashTime;

	bool isDashing = false;
	bool dashF = false;
	bool dashB = false;
	bool dashL = false;
	bool dashR = false;
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
				KeyCode.O,
				KeyCode.P
			});
		}
		currentDashTime = maxDashTime;
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
		if (Input.GetKeyDown (playerKeys [0])) {
			if ( FButtonCooler > 0 && FButtonCount == 1 && isDashing == false){
				currentDashTime = 0.0f;
				isDashing = true;
				dashF = true;
			}else{
				FButtonCooler = 0.5f ; 
				FButtonCount += 1 ;
			}
		}
		if (Input.GetKey (playerKeys[1])) {
			movementVector += -transform.right * movementSpeed;
		} 
		if (Input.GetKeyDown (playerKeys [1])) {
			if ( LButtonCooler > 0 && LButtonCount == 1 && isDashing == false){
				currentDashTime = 0.0f;
				isDashing = true;
				dashL = true;
			}else{
				LButtonCooler = 0.5f ; 
				LButtonCount += 1 ;
			}
		}
		if (Input.GetKey (playerKeys[2])) {
			movementVector += -transform.forward * movementSpeed;
		} 
		if (Input.GetKeyDown (playerKeys [2])) {
			if ( BButtonCooler > 0 && BButtonCount == 1 && isDashing == false){
				currentDashTime = 0.0f;
				isDashing = true;
				dashB = true;
			}else{
				BButtonCooler = 0.5f ; 
				BButtonCount += 1 ;
			}
		}
		if (Input.GetKey (playerKeys[3])) {
			movementVector += transform.right * movementSpeed;
		} 
		if (Input.GetKeyDown (playerKeys [3])) {
			if ( RButtonCooler > 0 && RButtonCount == 1 && isDashing == false){
				currentDashTime = 0.0f;
				isDashing = true;
				dashR = true;
			}else{
				RButtonCooler = 0.5f ; 
				RButtonCount += 1 ;
			}
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
				//movementVector.y = jumpPower;
			}
			
		}

		if ( FButtonCooler > 0 )
		{
			FButtonCooler -= 1 * Time.deltaTime ;
		}else{
			FButtonCount = 0 ;
		}
		if ( LButtonCooler > 0 )
		{
			LButtonCooler -= 1 * Time.deltaTime ;
		}else{
			LButtonCount = 0 ;
		}
		if ( RButtonCooler > 0 )
		{
			RButtonCooler -= 1 * Time.deltaTime ;
		}else{
			RButtonCount = 0 ;
		}
		if ( BButtonCooler > 0 )
		{
			BButtonCooler -= 1 * Time.deltaTime ;
		}else{
			BButtonCount = 0 ;
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
		if (isDashing) {
			movementVector = Vector3.zero;
			if (currentDashTime < maxDashTime)
			{
				if (dashF){
					movementVector += transform.forward * dashSpeed;
				}
				else if (dashB){
					movementVector += -transform.forward * dashSpeed;
				}
				else if (dashL){
					movementVector += -transform.right * dashSpeed;
				}
				else if (dashR){
					movementVector += transform.right * dashSpeed;
				}
				currentDashTime += dashStoppingSpeed;
			}
			else {
				isDashing = false;
				dashF = false;
				dashB = false;
				dashL = false;
				dashR = false;
			}
			//else
			//{
			//	movementVector = Vector3.zero;
			//}
		}

		//movementVector.y -= gravity * Time.deltaTime;
		characterController.Move (movementVector * Time.deltaTime);
		transform.LookAt (opponent);
		transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y,transform.localEulerAngles.z);

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