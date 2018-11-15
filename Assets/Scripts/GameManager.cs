using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Game States
public enum GameState { INTRO, MAIN_MENU, PAUSED, GAME, CREDITS, HELP }

public delegate void OnStateChangeHandler();


public class GameManager : MonoBehaviour
{
	protected GameManager() {}
	//This is used as a callback when a game state changes. eg when a scene changes
	public static GameManager instance = null;
	public event OnStateChangeHandler OnStateChange;
	//This is an attribute that is a getter for the current game state, it gets the current state and sets it
	public GameState gameState { get; private set; }

	public static GameManager Instance
	{
		get
		{
			if (GameManager.instance == null)
			{
				DontDestroyOnLoad(GameManager.instance);
				GameManager.instance = new GameManager();
			}
			return GameManager.instance;
		}
	}
	//This method is used to change the game state which only requires an enum variable as a parameter
	//It sets the new gameState for the instance and calls the callback OnStateChange method.
	public void SetGameState(GameState state)
	{
		this.gameState = state;
		OnStateChange();
	}



	public PlayerController controller;

	//This is an int for each game event which will have an event ID essentially.
	//public int gameEventID = 0;

	//Static instance of GameManager which allows it to be accessed by any other script.

	public GameObject thePlayer;
	private ScoreManager theScoreManager;
	public bool gameIsRunning;
	private WaveSpawner theWaveSpawner;

	public void SetPlayerController(PlayerController c)
	{
		controller = c;
	}


	//Awake is always called before any Start functions
	/*void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)
		{

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
		}
	}*/


	// Use this for initialization
	void Start ()
	{
		//SceneManager.LoadScene("");

		gameIsRunning = true;
		theScoreManager = FindObjectOfType<ScoreManager>();
		//theWaveSpawner = FindObjectOfType<WaveSpawner>();
	}

	

	public void RestartGame()
	{
		StartCoroutine("RestartGameCo");
	}

	
	public void OnApplicationQuit()
	{
		GameManager.instance = null;
	}

	//Restart the game
	public IEnumerator RestartGameCo()
	{
		theScoreManager.scoreIncreasing = false;
		thePlayer.SetActive(false);
		yield return new WaitForSeconds(0.5f);

		thePlayer.SetActive(true);
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
	}
	//Pause game
	private IEnumerator Pause(int p)
	{
		gameIsRunning = false;
		Time.timeScale = 0.1f;
		float pauseEndTime = Time.realtimeSinceStartup + 1;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1;
	}


}
