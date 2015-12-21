using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	// references to top and bottom character separately
	public GameObject topCharacter;
	public GameObject bottomCharacter;

	// personal player number
	public int playerNumber;

	// reference to opponent
	public Transform opponent;

	// set a movespeed
	public float movementSpeed = 32;

	// particle system
	public ParticleSystem particles;

	// player key display stuff
	public GameObject[] wasdImages;
	public GameObject[] arrowImages;
	Vector3 movementVector;
	CharacterController characterController;

	// animator references
	Animator topPlayerAnimator;
	Animator bottomPlayerAnimator;

	// variables for dashing logic
	float FButtonCooler = 0.5f ;
	float FButtonCount = 0f;
	float BButtonCooler = 0.5f ;
	float BButtonCount = 0f;
	float LButtonCooler = 0.5f ;
	float LButtonCount = 0f;
	float RButtonCooler = 0.5f ;
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

	// key mapping
	List<KeyCode> playerKeys;

	// define character mass
	float mass = 3.0F;

	// impact vector
	Vector3 impact = Vector3.zero;

	void Start ()
	{
		// initialize
		characterController = GetComponent<CharacterController> ();
		topPlayerAnimator = topCharacter.GetComponent<Animator> ();
		bottomPlayerAnimator = bottomCharacter.GetComponent<Animator> ();

		if (playerNumber == 1) {
			playerKeys = new List<KeyCode> (new KeyCode[] {
				KeyCode.W,
				KeyCode.A,
				KeyCode.S,
				KeyCode.D,
				KeyCode.T
			});
		} else {
			playerKeys = new List<KeyCode> (new KeyCode[] {
				KeyCode.UpArrow,
				KeyCode.LeftArrow,
				KeyCode.DownArrow,
				KeyCode.RightArrow,
				KeyCode.Slash
			});
		}
		currentDashTime = maxDashTime;
	}
		
	void Update ()
	{
		movementVector = new Vector3 (0f, movementVector.y, 0f);

		if ((Input.GetKey (playerKeys [0])) || 
			(Input.GetKey (playerKeys [1])) ||
			(Input.GetKey (playerKeys [2])) ||
			(Input.GetKey (playerKeys [3]))) {
			bottomPlayerAnimator.SetBool ("IsWalking", true);
		} else {
			bottomPlayerAnimator.SetBool ("IsWalking", false);
		}

		//Movement direction FORWARD, BACK, LEFT, RIGHT
		if (Input.GetKey (playerKeys [0])) {
			movementVector += transform.forward * movementSpeed;
		}
		if (Input.GetKeyDown (playerKeys [0])) {
			if (FButtonCooler > 0 && FButtonCount == 1 && isDashing == false) {
				// do double tap stuff
				currentDashTime = 0.0f;
				isDashing = true;
				dashF = true;
			} else {
				// timer
				FButtonCooler = 0.5f; 
				FButtonCount += 1;
			}
		}
		if (Input.GetKey (playerKeys [1])) {
			movementVector += -transform.right * movementSpeed;
		} 
		if (Input.GetKeyDown (playerKeys [1])) {
			if (LButtonCooler > 0 && LButtonCount == 1 && isDashing == false) {
				currentDashTime = 0.0f;
				isDashing = true;
				dashL = true;
			} else {
				LButtonCooler = 0.5f; 
				LButtonCount += 1;
			}
		}
		if (Input.GetKey (playerKeys [2])) {
			movementVector += -transform.forward * movementSpeed;
		} 
		if (Input.GetKeyDown (playerKeys [2])) {
			if (BButtonCooler > 0 && BButtonCount == 1 && isDashing == false) {
				currentDashTime = 0.0f;
				isDashing = true;
				dashB = true;
			} else {
				BButtonCooler = 0.5f; 
				BButtonCount += 1;
			}
		}
		if (Input.GetKey (playerKeys [3])) {
			movementVector += transform.right * movementSpeed;
		} 
		if (Input.GetKeyDown (playerKeys [3])) {
			if (RButtonCooler > 0 && RButtonCount == 1 && isDashing == false) {
				currentDashTime = 0.0f;
				isDashing = true;
				dashR = true;
			} else {
				RButtonCooler = 0.5f; 
				RButtonCount += 1;
			}
		}

		if (FButtonCooler > 0) {
			FButtonCooler -= 1 * Time.deltaTime;
		} else {
			FButtonCount = 0;
		}
		if (LButtonCooler > 0) {
			LButtonCooler -= 1 * Time.deltaTime;
		} else {
			LButtonCount = 0;
		}
		if (RButtonCooler > 0) {
			RButtonCooler -= 1 * Time.deltaTime;
		} else {
			RButtonCount = 0;
		}
		if (BButtonCooler > 0) {
			BButtonCooler -= 1 * Time.deltaTime;
		} else {
			BButtonCount = 0;
		}

		//Attack Logic PLAY ANIMATION TO ATTACK
		if (Input.GetKeyDown (playerKeys [4])) {
			topPlayerAnimator.SetTrigger ("AttackTrigger");
		} 

		//Apply final movement vector
		if (isDashing) {
			movementVector = Vector3.zero;
			particles.Play ();
			if (currentDashTime < maxDashTime) {
				if (dashF) {
					movementVector += transform.forward * dashSpeed;
				} else if (dashB) {
					movementVector += -transform.forward * dashSpeed;
				} else if (dashL) {
					movementVector += -transform.right * dashSpeed;
				} else if (dashR) {
					movementVector += transform.right * dashSpeed;
				}
				currentDashTime += dashStoppingSpeed;
			} else {
				// close particles
				particles.Stop ();
				isDashing = false;
				dashF = false;
				dashB = false;
				dashL = false;
				dashR = false;
			}
		}

		characterController.Move (movementVector * Time.deltaTime);
		transform.LookAt (opponent);
		transform.localEulerAngles = new Vector3 (0f, transform.localEulerAngles.y, transform.localEulerAngles.z);

		if (impact.magnitude > 0.2F) {
			characterController.Move (impact * Time.deltaTime);
		}
		// consumes the impact energy each cycle:
		impact = Vector3.Lerp (impact, Vector3.zero, 5 * Time.deltaTime);


		// key images
		if (playerNumber == 1) {
			if (Input.GetKey (playerKeys [0])) {
				wasdImages [0].SetActive (true);
				wasdImages [1].SetActive (false);
				wasdImages [2].SetActive (false);
				wasdImages [3].SetActive (false);
			} else if (Input.GetKey (playerKeys [1])) {
				wasdImages [0].SetActive (false);
				wasdImages [1].SetActive (true);
				wasdImages [2].SetActive (false);
				wasdImages [3].SetActive (false);
			} else if (Input.GetKey (playerKeys [2])) {
				wasdImages [0].SetActive (false);
				wasdImages [1].SetActive (false);
				wasdImages [2].SetActive (true);
				wasdImages [3].SetActive (false);
			} else if (Input.GetKey (playerKeys [3])) {
				wasdImages [0].SetActive (false);
				wasdImages [1].SetActive (false);
				wasdImages [2].SetActive (false);
				wasdImages [3].SetActive (true);
			} else {
				wasdImages [0].SetActive (false);
				wasdImages [1].SetActive (false);
				wasdImages [2].SetActive (false);
				wasdImages [3].SetActive (false);
			}
		} else if (playerNumber == 2) {
			if (Input.GetKey (playerKeys [0])) {
				arrowImages [0].SetActive (true);
				arrowImages [1].SetActive (false);
				arrowImages [2].SetActive (false);
				arrowImages [3].SetActive (false);
			} else if (Input.GetKey (playerKeys [1])) {
				arrowImages [0].SetActive (false);
				arrowImages [1].SetActive (true);
				arrowImages [2].SetActive (false);
				arrowImages [3].SetActive (false);
			} else if (Input.GetKey (playerKeys [2])) {
				arrowImages [0].SetActive (false);
				arrowImages [1].SetActive (false);
				arrowImages [2].SetActive (true);
				arrowImages [3].SetActive (false);
			} else if (Input.GetKey (playerKeys [3])) {
				arrowImages [0].SetActive (false);
				arrowImages [1].SetActive (false);
				arrowImages [2].SetActive (false);
				arrowImages [3].SetActive (true);
			} else {
				arrowImages [0].SetActive (false);
				arrowImages [1].SetActive (false);
				arrowImages [2].SetActive (false);
				arrowImages [3].SetActive (false);
			}
		}
	}

	// call this function to add an impact force:
	public void AddImpact (Vector3 dir, float force)
	{
		dir.Normalize ();
		if (dir.y < 0)
			dir.y = -dir.y; // reflect down force on the ground
		impact += dir.normalized * force / mass;

	}
}