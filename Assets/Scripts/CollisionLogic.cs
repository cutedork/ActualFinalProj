using UnityEngine;
using System.Collections;

public class CollisionLogic : MonoBehaviour {

	int hitcount;

	void Start(){
		hitcount = 3;
	}

	void OnTriggerEnter(Collider collision){

		Debug.Log ("COLLISION!");
		if (collision.gameObject.tag == "Arm") {
			Debug.Log ("ARM!");
			hitcount--;

			if (hitcount <= 0){
				Destroy (GetComponent<HingeJoint>());
				GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, -1f)) * 1000f);
			}
		}
	}
}
