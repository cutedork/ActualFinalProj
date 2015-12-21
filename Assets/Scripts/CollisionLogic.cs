using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollisionLogic : MonoBehaviour
{
	// personal player number
	public int playerNumber;

	// audio
	public AudioClip scream;
	public AudioClip death;
	public AudioSource sounds;

	// UI
	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;

	// opponent references
	public GameObject Opponent;
	public GameObject OpponentTopBody;

	// personal variables
	int hitcount;
	string colliderName;
	float lastTimeHit;
	bool endGame;
	ParticleSystem playerHitParticle;
	PlayerMovement playerMovement;

	void Start ()
	{
		// initializing personal variables
		playerMovement = GetComponentInParent<PlayerMovement> ();
		if (playerMovement == null) {
			Debug.LogError ("player movement script is null");
		}

		lastTimeHit = 0f;
		hitcount = 3;
		endGame = false;
		playerHitParticle = gameObject.GetComponent<ParticleSystem> ();
		playerHitParticle.Stop ();
		sounds = gameObject.GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider collision)
	{
		// if statement to check if it's hitting itself
		if ((tag == "Player1" && collision.tag == "Player2") || (tag == "Player2" && collision.tag == "Player1")) {
			// if statement to check cooldown between hits
			// if ((Time.time - GetComponentInParent<PlayerMovement> ().lastTrigger) < 0.5f) {
			if (Time.time - lastTimeHit > 0.5f) {
				// play particles
				playerHitParticle.Play ();
				// set screenshake
				CameraZoom.isScreenShaking = true;
				// add knockback effect
				Opponent.GetComponent<PlayerMovement> ().AddImpact (-Opponent.GetComponent<Transform> ().forward, 1000f);

				// gain access to the other collisionlogic script
				CollisionLogic otherCollisionLogic = collision.gameObject.GetComponent<CollisionLogic> ();

				// somewhat manually set the hp bars
				int otherHitcount = otherCollisionLogic.hitcount;
				otherHitcount--;
				if (otherHitcount == 2) {
					otherCollisionLogic.ball1.SetActive (false);
					sounds.PlayOneShot (scream);
				}
				if (otherHitcount == 1) {
					otherCollisionLogic.ball2.SetActive (false);
					sounds.PlayOneShot (scream);
				}
				if (otherHitcount == 0) {
					otherCollisionLogic.ball3.SetActive (false);
					sounds.PlayOneShot (death);
				}
				collision.gameObject.GetComponent<CollisionLogic> ().hitcount = otherHitcount;
				lastTimeHit = Time.time;

				if (otherHitcount <= 0) {
					// knock other player off
					OpponentTopBody.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
					OpponentTopBody.GetComponent<Rigidbody> ().AddForce ((new Vector3 (0f, 1f, -1f)) * 1000f);

					if (endGame == false) {
						endGame = true;
						StartCoroutine (EndGame ());
					}
				}
			}
			//}
		}
	}

	IEnumerator EndGame ()
	{
		yield return new WaitForSeconds (1f);
		if (playerNumber == 1) {
			Application.LoadLevel ("Player1WinScene");
		} else {
			Application.LoadLevel ("Player2WinScene");
		}
	}
	
}
