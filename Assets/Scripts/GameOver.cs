using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Text gameOver;
	public Text gameOverScore;

	public static GameOver instance;

	public GameObject gameOverPanel;

	void Awake (){
		instance = this;
	}

	public void ShowGameOver(bool check){
		gameOverScore.text = ScoreTracker.instance.Score.ToString ();
		gameOverPanel.SetActive (check);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
