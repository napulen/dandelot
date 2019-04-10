using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Statistics : MonoBehaviour {
	public Text score;
	public Text bestStreak;
	public Text correctNotes;
	public Text misspelledNotes;
	// Use this for initialization
	void Start ()
	{
		score.text += StaticClass.Score.ToString();
		bestStreak.text += StaticClass.BestStreak.ToString();
		correctNotes.text += StaticClass.CorrectNotes.ToString();
		misspelledNotes.text += StaticClass.MisspelledNotes.ToString();
		Invoke("ReloadGame", 5);
	}

	void ReloadGame()
	{
		SceneManager.LoadScene("Menu");
	}
}
