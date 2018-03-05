using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

	public float fadeSpeed;

	// Use this for initialization
	public void Fade (){
		gameObject.SetActive (true);
		StartCoroutine (DoFade ());
	}

	IEnumerator DoFade (){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup> ();
		while (canvasGroup.alpha < 1) {
			canvasGroup.alpha += Time.deltaTime * fadeSpeed;
			yield return null;
		}
		canvasGroup.interactable = true;
		yield return null;
	}
}
