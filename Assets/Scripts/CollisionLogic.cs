using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollisionLogic : MonoBehaviour {

	// LEGIT UGLY
	public int playerNumber;

	int hitcount;
	string colliderName;
	float lastTimeHit;
	bool endGame;

	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;
	bool isBall1Active = true;
	bool isBall2Active = true;
	bool isBall3Active = true;

	



	void Start(){


		// literally hardcoding, I'll rewrite this later for non-prototype
		if (playerNumber == 1) {
			colliderName = "Player2";
		} else {
			colliderName = "Player1";
		}
		lastTimeHit = 0f;
		hitcount = 3;
		endGame = false;
	}

	void OnTriggerEnter(Collider collision){
		// LITERALLY. HARDCODING.
		// two arms = this is being calling twice LOL #goodenough
		if (collision.gameObject.name == colliderName) {
			Debug.Log (lastTimeHit);
			Debug.Log (Time.time);
			if (Time.time - lastTimeHit > 1){
				string testString = "Ouch from " + playerNumber.ToString();
				Debug.Log (testString);
				hitcount--;
				if( hitcount == 2)
				{
					ball1.SetActive(false);
				}
				if( hitcount == 1 )
				{
					ball2.SetActive(false);

				}
				if(hitcount == 0)
				{
					ball3.SetActive(false);

				}


				lastTimeHit = Time.time;

				if (hitcount <= 0){
					Debug.Log ("WTF?!");
					// knock top player off... somehow
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
					GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, -1f)) * 1000f);

					if (endGame == false){
						endGame = true;
						StartCoroutine("EndGame");
					}
					//Application.LoadLevel(2);
				}
			}
		}
	}

	public IEnumerator EndGame()
	{
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(2);
	}
	
}
