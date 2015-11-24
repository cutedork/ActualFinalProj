using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionLogic : MonoBehaviour {
	int hitcount;
	void Start(){
		hitcount = 3;
	}

	void OnTriggerEnter(Collider collision){
		Debug.Log ("COLLISION!");
		Debug.Log (collision.gameObject.name);
		if (collision.gameObject.tag == "Arm") {
			Debug.Log ("ARM!");
			hitcount--;

			if (hitcount <= 0){
				// knock top player off
				//GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, -1f)) * 1000f);
			}
		}
	}
	
}
