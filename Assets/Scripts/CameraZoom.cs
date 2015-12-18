using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public Transform player1;
	public Transform player2;

	public static bool isScreenShaking = false;

	Vector3 midpoint;
	Vector3 offset;
	float minimumOrthoSize;
	float distance;
	float cameraSpeed;
	
	// Use this for initialization
	void Start () {
		offset = new Vector3 (142f, 132f, -170f); 
		minimumOrthoSize = 20f;
	}

	void LateUpdate()
	{
		transform.position = CalculateCameraPosition();
		//Debug.Log (Vector3.Distance (player1.transform.position, player2.transform.position));
		StartCoroutine(drawMyLine(player1.transform.position , player2.transform.position, Color.red , 0.01f));

		GetComponent<Camera>().orthographicSize = CalculateOrthographicSize();
	}


	Vector3 CalculateCameraPosition(){
		midpoint = new Vector3 ((player1.transform.position.x + player2.transform.position.x) / 2,
		                        (player1.transform.position.y + player2.transform.position.y) / 2,
		                        (player1.transform.position.z + player2.transform.position.z) / 2);
		Debug.DrawLine(player1.transform.position, midpoint, Color.red);
		Vector3 finalPosition = midpoint + offset;
		if (isScreenShaking){
			Debug.Log ("SHAKE");
			float t = 0.25f; // t = time
			while ( t > 0f ) { // as long as t > 0, then keep doing this code...
				t -= Time.deltaTime; // each frame, make t smaller ( divide this by / 2f to change duration of screen shake
				// Time.deltaTime = time last frame, usually 1/60th of a second
				// eventually t will be less than or equal to zero
				Vector3 shakeVector = transform.right * Mathf.Sin (Time.time * 500f) + transform.up * Mathf.Sin (Time.time * 1000f); 
				finalPosition += shakeVector * t * 0.25f;
				if ( t <= 0f ){
					isScreenShaking = false;
				}
			}
		}
		return finalPosition;
	}

	float CalculateOrthographicSize(){
		distance = Vector3.Distance(player1.transform.position, player2.transform.position);
		return (minimumOrthoSize + (distance/5f)); // original 7f
	}

	IEnumerator drawMyLine(Vector3 start , Vector3 end, Color color,float duration = 0.1f){
		GameObject myLine = new GameObject ();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer> ();
		LineRenderer lr = myLine.GetComponent<LineRenderer> ();
		lr.material = new Material (Shader.Find ("Particles/Additive"));
		lr.SetColors (color,color);
		lr.SetWidth (0.5f,0.5f);
		lr.SetPosition (0, start);
		lr.SetPosition (1, end);
		yield return new WaitForSeconds(duration);
		GameObject.Destroy (myLine);
	}

}
