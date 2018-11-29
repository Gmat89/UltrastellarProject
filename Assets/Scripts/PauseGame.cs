﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	private GameManager theGameManager;
	private ScoreManager theScoreManager;


	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
		theGameManager.GetComponent<GameManager>().enabled = false;
		theScoreManager = gameObject.GetComponentInChildren<ScoreManager>();
		theScoreManager.enabled = false;

	}

	public void QuitGame()
	{
		Debug.Log("Quitting Game");
		Application.Quit();
	}
}

