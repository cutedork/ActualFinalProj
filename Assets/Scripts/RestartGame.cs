using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			Application.LoadLevel ("Level1Scene");
		}

	}
}
