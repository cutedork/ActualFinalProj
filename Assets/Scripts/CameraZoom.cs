using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{

	// give it access to both player1 and player2
	public Transform player1;
	public Transform player2;

	// global for screenshake so CollisionLogic can set it when players get hit
	public static bool isScreenShaking = false;

	// variables for autoadjusting camera calculations
	Vector3 midpoint;
	Vector3 offset;
	float minimumOrthoSize;
	float distance;
	float cameraSpeed;

	void Start ()
	{
		// initialize a custom offset
		offset = new Vector3 (142f, 132f, -170f); 
		// minimum orthographic size so it doesn't seem like everything is super zoomed in
		minimumOrthoSize = 20f;
	}

	void LateUpdate ()
	{
		// calculate position + orthographic size
		transform.position = CalculateCameraPosition ();
		GetComponent<Camera> ().orthographicSize = CalculateOrthographicSize ();
		// check if it's in tutorial mode, if it is, draw lines between the players
		if (TutorialManager.isTutorial) {
			Vector3 newEnd;
			Vector3 newStart = player1.transform.position;
			Vector3 offset = (player2.transform.position - player1.transform.position) / 100;
			for (int i = 50; i > 0; i--) {
				newEnd = newStart + offset;
				StartCoroutine (drawMyLine (newStart, newEnd, Color.red));
				newStart = newEnd + offset;
			}
		}
	}

	Vector3 CalculateCameraPosition ()
	{
		// calculate a midpoint between two players
		midpoint = new Vector3 ((player1.transform.position.x + player2.transform.position.x) / 2,
		                        (player1.transform.position.y + player2.transform.position.y) / 2,
		                        (player1.transform.position.z + player2.transform.position.z) / 2);
		// add an offset to the midpoint
		// this causes an effect of a camera floating above that auto-adjusts as players move
		Vector3 finalPosition = midpoint + offset;
		// if screen is supposed to be shaking run this code
		if (isScreenShaking) {
			// set a shake time
			float t = 0.25f; // t = time
			while (t > 0f) { // as long as t > 0, then keep doing this code...
				t -= Time.deltaTime; // each frame, make t smaller ( divide this by / 2f to change duration of screen shake
				// Time.deltaTime = time last frame, usually 1/60th of a second
				// eventually t will be less than or equal to zero
				Vector3 shakeVector = transform.right * Mathf.Sin (Time.time * 500f) + transform.up * Mathf.Sin (Time.time * 1000f); 
				finalPosition += shakeVector * t * 0.25f;
				if (t <= 0f) {
					isScreenShaking = false;
				}
			}
		}
		// return final camera position
		return finalPosition;
	}

	float CalculateOrthographicSize ()
	{
		// calculate how far the camera should be depending on the distance between players
		// the effect of this looks like the camera zooms out dynamically as players get farther
		// or zooms in as players get closer
		distance = Vector3.Distance (player1.transform.position, player2.transform.position);
		return (minimumOrthoSize + (distance / 5f)); // original 7f
	}

	// simple code to draw lines
	IEnumerator drawMyLine (Vector3 start, Vector3 end, Color color)
	{
		// create a game object at a start location and add a line renderer
		GameObject myLine = new GameObject ();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer> ();
		LineRenderer lr = myLine.GetComponent<LineRenderer> ();
		// set line renderer properties
		lr.material = new Material (Shader.Find ("Particles/Additive"));
		lr.SetColors (color, color);
		lr.SetWidth (0.5f, 0.5f);
		lr.SetPosition (0, start);
		lr.SetPosition (1, end);
		yield return 0;
		// destroy the line after
		GameObject.Destroy (myLine);
	}

}
