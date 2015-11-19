using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	Animator topPlayerAnimator;

	bool playAnim = false;

	// Use this for initialization
	void Start () {

		topPlayerAnimator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

//		if (Input.GetKey (KeyCode.Space)) {
//			topPlayerAnimator.SetBool ("IsAttacking", true); 
//		} else {
//			topPlayerAnimator.SetBool ("IsAttacking", false);
//		}

		if (Input.GetKeyDown(KeyCode.A)) {
			playAnim = !playAnim;
		} 

		topPlayerAnimator.SetBool ("IsAttacking", playAnim); 
	
	}
}
