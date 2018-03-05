using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapSphere : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private Vector3 origin;

	private bool touched;

//	public GameObject sphere;

	void Awake (){
		touched = false;
	}
				
	public void OnPointerDown (PointerEventData data){
//		origin = data.position;
		if (!touched) {
			touched = true;
			origin = Camera.main.ScreenToWorldPoint (data.position);
//			Instantiate (sphere, origin, Quaternion.Euler (90, 0, 0));
		}
	}

	public void OnPointerUp (PointerEventData data) {
		touched = false;
	}
}
