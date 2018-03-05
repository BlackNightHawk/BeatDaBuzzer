using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteProject;

public class TouchManager : MonoBehaviour {
	//This Script will not handle touches and interactions. Will replace, OnContact and TapShere scripts.

	private GameController gameController;

	public GameObject pulse;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			Vector3 mousePosFar = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
			Vector3 mousePosNear = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
			Vector3 mousePosF = Camera.main.ScreenToWorldPoint (mousePosFar);
			Vector3 mousePosN = Camera.main.ScreenToWorldPoint (mousePosNear);

			RaycastHit hit;

			if (Physics.Raycast (mousePosN, mousePosF - mousePosN, out hit)) {
				Destroy(hit.transform.gameObject);
				Instantiate (pulse, hit.transform.position, hit.transform.rotation);
				gameController.GameOver();
			}
		}	
	}
}
