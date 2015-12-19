using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel ("Level1Scene");
		}

	}
}
