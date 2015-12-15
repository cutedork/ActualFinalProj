using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
	
	public float shakePower = 0.25f;
	
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ( KeyCode.Space) ) {
			StartCoroutine (ShakeCoroutine (shakePower) ) ;
		}
		
	}
	
	public void DoScreenShake () {  
		StartCoroutine (ShakeCoroutine (0.25f) ); 
	}
	
	IEnumerator ShakeCoroutine (float shakePower) {
		Vector3 cameraStart = Camera.main.transform.position; 
		float t = 1f; // t = time
		while ( t > 0f ) { // as long as t > 0, then keep doing this code...
			t -= Time.deltaTime / 0.5f; // each frame, make t smaller ( divide this by / 2f to change duration of screen shake
			// Time.deltaTime = time last frame, usually 1/60th of a second
			// eventually t will be less than or equal to zero
			Vector3 shakeVector = Camera.main.transform.right * Mathf.Sin (Time.time * 50f) + 
				Camera.main.transform.up * Mathf.Sin (Time.time * 100f); 
			Camera.main.transform.position = cameraStart + shakeVector * t * shakePower;
			yield return 0;
		}
	}
}
