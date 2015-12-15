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
		if (!isScreenShaking){
			transform.position = CalculateCameraPosition();
			//Debug.Log (Vector3.Distance (player1.transform.position, player2.transform.position));
			GetComponent<Camera>().orthographicSize = CalculateOrthographicSize();
		}
	}


	Vector3 CalculateCameraPosition(){
		midpoint = new Vector3 ((player1.transform.position.x + player2.transform.position.x) / 2,
		                        (player1.transform.position.y + player2.transform.position.y) / 2,
		                        (player1.transform.position.z + player2.transform.position.z) / 2);
		Debug.DrawLine(player1.transform.position, midpoint, Color.red);
		return (midpoint + offset);
	}

	float CalculateOrthographicSize(){
		distance = Vector3.Distance(player1.transform.position, player2.transform.position);
		return (minimumOrthoSize + (distance/5f)); // original 7f
	}

}
