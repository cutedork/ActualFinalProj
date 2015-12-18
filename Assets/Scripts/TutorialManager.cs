	using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	public static bool isTutorial = true;
	public GameObject p1Keys;
	public GameObject p2Keys;
	
	void Update () {
		StartCoroutine(endTutorial (10.0f));
	}

	IEnumerator endTutorial( float duration ){
		yield return new WaitForSeconds (duration);
		isTutorial = false;
		p1Keys.SetActive (false);
		p2Keys.SetActive (false);
	}
}
