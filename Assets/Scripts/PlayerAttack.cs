using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	Animator topPlayerAnimator;

	bool playAnim = false;
	public float hitTimer;

	// Use this for initialization
	void Start () {
		hitTimer = 0f;
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
			hitTimer = 0.5f;
		} 
		if (hitTimer > 0) {
			hitTimer -= Time.deltaTime;
		}

		topPlayerAnimator.SetBool ("IsAttacking", playAnim); 
	
	
	}
}
