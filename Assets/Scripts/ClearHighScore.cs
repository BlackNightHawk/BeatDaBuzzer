using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearHighScore : MonoBehaviour {

	// Use this for initialization
	public void ResetHighScore (){
		PlayerPrefs.SetFloat ("highScore", 99999);
		Debug.Log ("Highscore has been reset");
	}
}
