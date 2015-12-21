using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel ("GameScene");
		}

	}
}
