using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

	public static ScoreTracker instance;
	private int _score;
	public Text ScoreText;
	public Text HighScoreText;

	void Awake(){
		instance = this;

		if (!PlayerPrefs.HasKey("HighScore")){
			PlayerPrefs.SetInt ("HighScore", 0);
		}

		ScoreText.text = "0";
		HighScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();
	}

	public int Score {
		get {
			return _score;
		}

		set {
			_score = value;
			ScoreText.text = _score.ToString ();
			if (PlayerPrefs.GetInt ("HighScore") < _score) {
				PlayerPrefs.SetInt ("HighScore", _score);
				HighScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();
			}
		}

	}
}
