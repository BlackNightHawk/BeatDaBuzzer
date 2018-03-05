using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

	public float fadeSpeed;

	// Use this for initialization
	public void Fade(){
		StartCoroutine (DoFade ());
	}

	IEnumerator DoFade (){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup> ();
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
			yield return null;
		}
		canvasGroup.interactable = false;
		gameObject.SetActive (false);
		yield return null;
	}
}
