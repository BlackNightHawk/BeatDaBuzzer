using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

namespace CompleteProject
{

	public class GameController : MonoBehaviour {

		public GameObject buzzer;

		public Vector3 spawnValues;
		private float spawnZ;

		public float startWaitMin;
		public float startWaitMax;
		private float startWait;

		private bool gameOver;
		private bool waiting;

		public Text timerText;
		private float reactTime;

		private float highScore;
		private int playedBefore;

		// For End Menu
		public GameObject restartButton;
		public GameObject scoreOutline;
		public Text scoreText;
		public Text highScoreText;

		// For Start Menu
	//	public GameObject startOverlay;
		public GameObject startButton;
		public GameObject titleText;
		public GameObject infoText;
		public Text mainMenuScoreText;
		public GameObject removeAdsButton;
		public Text removeAdsText;

	//	public GameObject touchZone;

		private bool resetHS;

		//For Main menu Fade in
		public GameObject mainMenu;
		private FadeIn mainFadeIn;

		// For End Screen Fade IN
		public GameObject endScreen;
		private FadeIn fadeIn;

		//For determining if to show an ad
		private int showAd;

		//For playing waiting music
		private AudioSource waitingMusic;

		//For recording if the player premium
		public static bool premium = false;

		// Use this for initialization
		void Start () {
			resetHS = false;
			highScore = PlayerPrefs.GetFloat ("highScore");
			playedBefore = PlayerPrefs.GetInt ("playedBefore");
			timerText.text = "";
			gameOver = false;

			if (playedBefore == 0) {
				mainMenuScoreText.text = "Best Time: ";
			}
			if (playedBefore == 1) {
				mainMenuScoreText.text = "Best Time: " + highScore.ToString ();
			}

			waitingMusic = GetComponent<AudioSource> ();

			if (premium == true) {
				removeAdsText.text = "You Own Premium";
			}

			startWait = Random.Range (startWaitMin, startWaitMax);
			Debug.Log ("Start Wait is " + startWait);

			StartCoroutine(WaitForMenuFadeIn ());
		}

		void Update () {
			if (gameOver == false) {
				if (GameObject.FindGameObjectWithTag ("Buzzer") != null && waiting == false) {
					// Start Timer
	//				Debug.Log ("Timer is " + reactTime);
					reactTime += Time.deltaTime;
					timerText.text = "Time: " + reactTime;
				} 
			} 

			if (waiting == true) {
				timerText.text = "Time: ";
			}

			if (premium == true) {
				removeAdsText.text = "You Own Premium";
			} 
		}

		IEnumerator WaitForMenuFadeIn(){
			mainFadeIn = mainMenu.GetComponent<FadeIn> ();
			mainFadeIn.Fade();
			Debug.Log ("Main Screen Should be Fading in");
			yield return new WaitForSeconds (1);
			StartMenu ();
			yield break;
		}

		IEnumerator SpawnBuzzer (){
			Debug.Log ("Start SpawnBuzzer Corourtine");
			yield return new WaitForSeconds (startWait);
			Debug.Log ("Wait for has finished");
			spawnZ = Random.Range(-spawnValues.z, spawnValues.z);
			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnZ);
			Instantiate (buzzer, spawnPosition, Quaternion.identity);
			Debug.Log ("Buzzer has spawned");
			waitingMusic.Stop ();
			waiting = false;
			yield break;
		}

		void LoadHighScore(float newScore){
			if (playedBefore == 0) {
				if (newScore <= 99999) {
					highScore = newScore;
					PlayerPrefs.SetFloat ("highScore", highScore);
				}
			}
			if (playedBefore == 1) {
				if (newScore <= highScore) {
					highScore = newScore;
					PlayerPrefs.SetFloat ("highScore", highScore);
				}
			}
		}
			
		public void GameOver(){
			gameOver = true;
			LoadHighScore (reactTime);

			fadeIn = endScreen.GetComponent<FadeIn> ();
			fadeIn.Fade();
			Debug.Log ("End Scren Should be Fading in");

			scoreText.text = "" + reactTime;
			highScoreText.text = "Best Time: " + highScore.ToString ();
			timerText.text = "";

		}

		public void RestartGame(){
			StartCoroutine(WaitForFadeOut ());
		}

		IEnumerator WaitForFadeOut (){
			yield return new WaitForSeconds (1);
			Application.LoadLevel (Application.loadedLevel);
			showAd = Random.Range (0, 2);
			Debug.Log ("showAd is " + showAd);
			if (showAd == 1 && premium == false) {
				ShowAd ();
			}
			yield break;
		}

		void StartMenu(){
			highScore = PlayerPrefs.GetFloat ("highScore");
			timerText.text = "";
			removeAdsButton.SetActive (true);

		}

		public void StartGame(){
			mainMenuScoreText.text = "";
			PlayerPrefs.SetInt ("playedBefore", 1);
			timerText.text = "Time: ";
			waiting = true;
			waitingMusic.Play ();
			StartCoroutine(SpawnBuzzer ());

		}

		public void ResetHighScore (){
			PlayerPrefs.SetFloat ("highScore", 99999);
			Debug.Log ("Highscore has been reset");
			mainMenuScoreText.text = "Best Time: ";
		}

		public void ShowAd()
		{
			if (Advertisement.IsReady())
			{
				Advertisement.Show();
			}
		}
	}
}