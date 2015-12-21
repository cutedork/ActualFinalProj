using UnityEngine;
using System.Collections;

public class BeginGame : MonoBehaviour
{
	void Update ()
	{
		// start the game if space is pressed
		if (Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel ("GameScene");
		}
	
	}
}
