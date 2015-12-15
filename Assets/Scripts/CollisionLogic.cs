using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollisionLogic : MonoBehaviour {

	public ScreenShake shakeManager;

	// LEGIT UGLY
	public int playerNumber;

	int hitcount;
	string colliderName;
	float lastTimeHit;
	bool endGame;

	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;
    ParticleSystem playerHitParticle;

	//public Rigidbody rbody;
	public PlayerMovement playerMovement;
	public GameObject Opponent;
	public GameObject OpponentTopBody;


	void Start(){
		//rbody = GetComponent<Rigidbody> ();
		//if (rbody == null) {
		//	Debug.LogError ("rigidbody is null");
		//}
		playerMovement = GetComponentInParent<PlayerMovement> ();
		if (playerMovement == null) {
			Debug.LogError ("player movement script is null");
			}

		// literally hardcoding, I'll rewrite this later for non-prototype
	
	
		lastTimeHit = 0f;
		hitcount = 3;
		endGame = false;
		playerHitParticle = gameObject.GetComponent<ParticleSystem>();
		playerHitParticle.Stop ();

	}

	void OnTriggerEnter(Collider collision){
		if ((tag == "Player1" && collision.tag != "Player2") || (tag == "Player2" && collision.tag != "Player1")) {
			return;
		}

		// LITERALLY. HARDCODING.
		// two arms = this is being calling twice LOL #goodenough
		if ((Time.time - GetComponentInParent<PlayerMovement>().lastTrigger) < 0.5f) {
			Debug.Log (lastTimeHit);
			Debug.Log (Time.time);
			if (Time.time - lastTimeHit > 1){
				string testString = "Ouch from " + playerNumber.ToString();
				Debug.Log (testString);
				CollisionLogic otherCollisionLogic = collision.gameObject.GetComponent<CollisionLogic>();
				playerHitParticle.Play ();
				shakeManager.DoScreenShake(); // call screenshake

				Opponent.GetComponent<PlayerMovement>().AddImpact(-Opponent.GetComponent<Transform>().forward, 1000f);

				int otherHitcount = otherCollisionLogic.hitcount;
				otherHitcount--;
				if( otherHitcount == 2)
				{
					otherCollisionLogic.ball1.SetActive(false);
				}
				if( otherHitcount == 1 )
				{
					 otherCollisionLogic.ball2.SetActive(false);

				}
				if(otherHitcount == 0)
				{
					otherCollisionLogic.ball3.SetActive(false);

				}
				collision.gameObject.GetComponent<CollisionLogic>().hitcount = otherHitcount;
				lastTimeHit = Time.time;

				if (otherHitcount <= 0){
					Debug.Log ("WTF?!");
					// knock top player off... somehow
					//if (rbody){
			        gameObject.AddComponent<Rigidbody>();
					OpponentTopBody.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
					OpponentTopBody.GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, -1f)) * 1000f);
					//}

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
		if (playerNumber == 1) {
			Application.LoadLevel ("GameOver1");
		} else {
			Application.LoadLevel ("GameOver2");
		}
	}
	
}
