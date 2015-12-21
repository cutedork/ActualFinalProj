using UnityEngine;
using System.Collections;

public class IntroStart : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel ("HowToPlayScene");
		
		}
	}
}
