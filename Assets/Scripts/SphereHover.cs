using UnityEngine;
using System.Collections;

public class SphereHover : MonoBehaviour {
	
	Vector3 startPosition;
	
	// Use this for initialization
	void Start () {
		
		startPosition = transform.position; 
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// make sine wave 10 times faster, but move 10% of the distance 
		// transform.Translate (0f, Mathf.Sin (10f* Time.time) * 0.1f, 0f); 
		// .. but this method is going to produce drifting over time
		
		// this is much more safer, no drift, we are always hovering based on "start position"
		transform.position = startPosition + new Vector3(0f, Mathf.Sin (Time.time * 10f)* 0.6f, 0f);
		
		
		
	}
}
