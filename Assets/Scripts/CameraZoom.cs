using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	// attach to main camera
	public Transform player1Pos; 
	public Transform player2Pos;

	[SerializeField]
	Transform[] targets;

	[SerializeField] 
	float boundingBoxPadding = 2f;
	
	[SerializeField]
	float minimumOrthographicSize = 8f;
	
	[SerializeField]
	float zoomSpeed = 20f;
	
	Camera movingCamera;


	// Use this for initialization
	void Start () {
		movingCamera = Camera.main; 
		targets = new Transform[] {player1Pos, player2Pos};
	}

	void LateUpdate()
	{
		Rect boundingBox = CalculateTargetsBoundingBox();
		transform.position = CalculateCameraPosition(boundingBox);
		movingCamera.orthographicSize = CalculateOrthographicSize(boundingBox);
	}

	// calculate the orthographic "box" that has the objects you want to see
	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;
		
		foreach (Transform target in targets) {
			Vector3 position = target.position;
			
			minX = Mathf.Min(minX, position.x);
			minY = Mathf.Min(minY, position.y);
			maxX = Mathf.Max(maxX, position.x);
			maxY = Mathf.Max(maxY, position.y);
		}
		
		return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
	}

	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;
		
		return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, movingCamera.transform.position.z);
	}

	float CalculateOrthographicSize(Rect boundingBox)
	{
		float orthographicSize = movingCamera.orthographicSize;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
		Vector3 topRightAsViewport = movingCamera.WorldToViewportPoint(topRight);
		
		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(boundingBox.width) / movingCamera.aspect / 2f;
		else
			orthographicSize = Mathf.Abs(boundingBox.height) / 2f;
		
		return Mathf.Clamp(Mathf.Lerp(movingCamera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
	}

}
