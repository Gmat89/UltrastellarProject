using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	//Text object reference, score text
	public Text scoreText;
	//Text object reference, hi score text
	public Text hiScoreText;
	//current score
	public float scoreCount;
	//hi score count
	public float hiScoreCount;

	//points that the player will earn per second
	public float pointsPerSecond;
	//bool to check if the score is increasing
	public bool scoreIncreasing;

	// Use this for initialization
	void Start()
	{
		if (PlayerPrefs.HasKey("HighScore"))
		{
			//store the hiScore obtained by the player in player prefs
			hiScoreCount = PlayerPrefs.GetFloat("HighScore");
		}
	}

	// Update is called once per frame
	void Update()
	{
		//if the bool is true then the score will increase every second of gametime
		if (scoreIncreasing)
		{
			scoreCount += pointsPerSecond * Time.deltaTime;
		}

		//if the score is less that the hiscore then add + store the hiscore in player prefs
		if (scoreCount > hiScoreCount)
		{
			hiScoreCount = scoreCount;
			PlayerPrefs.SetFloat("HighScore", hiScoreCount);
		}
		//show the score textComponent values (unity does the math lol)
		scoreText.text = "Score: " + Mathf.Round(scoreCount);
		hiScoreText.text = "High Score: " + Mathf.Round(hiScoreCount);
	}

	//call this function to add points to score on whatever event
	public void AddScore(int pointsToAdd)
	{
		scoreCount += pointsToAdd;
	}

	void OnDestroy()
	{

	}
}

