using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		Destroy (other.gameObject);
	}
}
