using UnityEngine;
using System.Collections;

public class ArmAttack : MonoBehaviour {

	public GameObject leftShoulder;

	public GameObject rightShoulder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


#if UNITY_STANDALONE_OSX
		if (Input.GetButton ("LeftBumper_OSX")) {
			leftShoulder.transform.Rotate(10f, 0f, 0f);
		}
#endif

#if UNITY_STANDALONE_OSX
		if (Input.GetButton ("RightBumper_OSX")) {
			rightShoulder.transform.Rotate(-10f, 0f, 0f);
		}
#endif
	
	}
}
