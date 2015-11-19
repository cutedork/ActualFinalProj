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
		if (Input.GetButton ("LeftBumper_OSX") || Input.GetKey (KeyCode.Q)) {
			leftShoulder.transform.Rotate(10f, 0f, 0f);
		}
#endif

#if UNITY_STANDALONE_OSX
		if (Input.GetButton ("RightBumper_OSX") || Input.GetKey (KeyCode.E)) {
			rightShoulder.transform.Rotate(-10f, 0f, 0f);
		}
#endif

#if UNITY_STANDALONE_WIN
		if (Input.GetButton ("X")) {
			leftShoulder.transform.Rotate(10f, 0f, 0f);
		}
#endif
		
#if UNITY_STANDALONE_WIN
		if (Input.GetButton ("Y")) {
			rightShoulder.transform.Rotate(-10f, 0f, 0f);
		}
#endif

	
	}
}
