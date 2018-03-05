using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteProject;

public class OnContact : MonoBehaviour {

	private GameController gameController;

	public GameObject pulse;

	void Start (){
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
		Instantiate (pulse, gameObject.transform.position, gameObject.transform.rotation);
		gameController.GameOver();
	}
}
