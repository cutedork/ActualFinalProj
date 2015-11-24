﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionLogic : MonoBehaviour {

	// LEGIT UGLY
	public int playerNumber;

	int hitcount;
	string colliderName;
	float lastTimeHit;

	void Start(){
		// literally hardcoding, I'll rewrite this later for non-prototype
		if (playerNumber == 1) {
			colliderName = "Player2";
		} else {
			colliderName = "Player1";
		}
		lastTimeHit = 0f;
		hitcount = 3;
	}

	void OnTriggerEnter(Collider collision){
		// LITERALLY. HARDCODING.
		// two arms = this is being calling twice LOL #goodenough
		if (collision.gameObject.name == colliderName) {
			Debug.Log (lastTimeHit);
			Debug.Log (Time.time);
			if (Time.time - lastTimeHit > 1){
				string testString = "Ouch from " + playerNumber.ToString();
				Debug.Log (testString);
				hitcount--;

				lastTimeHit = Time.time;

				if (hitcount <= 0){
					Debug.Log ("WTF?!");
					// knock top player off... somehow
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
					GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, -1f)) * 1000f);
				}
			}
		}
	}
	
}
