using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// OnPlayerSpawn
	public event Action<PlayerController> OnPlayerSpawned;

	// OnPlayerdeath
	public event Action<PlayerController> OnPlayerHasDied;



	//Controller reference
	public PlayerController controller;

	//Reference to the PlayerController
	public GameObject thePlayerPrefab;

	//The Players start point
	public Vector3 playerStartPoint;

	//Reference to the Score Manager
	private ScoreManager theScoreManager;

	//Check if the player has spawned
	public bool playerSpawned;

	//Check if the game is running
	public bool gameIsRunning;

	//Reference to the Wave Spawner
	private WaveSpawner theWaveSpawner;

	public UIManager theUIManager;

	//Player Respawn effect
	public ParticleSystem respawnEffect;
	//Reference to the WaveSpawner Spawn Points
	//public Transform[] theEnemySpawnPoints;
	//public Vector3[] enemySpawnerStartPos;

	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;

	public GameObject currentPlayer;
	public int spawnPosAmount;

	public void SetPlayerController(PlayerController c)
	{
		controller = c;
	}



	public static GameManager instance = null
		; //Static instance of GameManager which allows it to be accessed by any other script.

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}


	// Use this for initialization
	void Start()
	{
		//SceneManager.LoadScene("");
		SpawnPlayer();
		//Set the game is running bool to true
		gameIsRunning = true;
		//find the ScoreManager
		theScoreManager = FindObjectOfType<ScoreManager>();
		//Find the wave spawner
		theWaveSpawner = FindObjectOfType<WaveSpawner>();
		//Subscribe to the OnPlayerDeath event and run the OnPlayerDead function when needed.
		//thePlayerPrefab.GetComponent<HealthManager>().OnPlayerDeath += RestartGame;
		theUIManager = FindObjectOfType<UIManager>();
		//enemySpawnerStartPos[0] = theEnemySpawnPoints[0].position;
		pauseMenuUI = GameObject.Find("PauseCanvas");
	}

	private void PlayerDeath(float obj)
	{
		//Unsub from their ondeath (or you'll get nulls)
		//currentPlayer.GetComponent<HealthManager>().OnPlayerDeath -= PlayerDeath;
		Destroy(currentPlayer);
		if (OnPlayerHasDied != null) OnPlayerHasDied(currentPlayer.GetComponent<PlayerController>());
		RestartGame();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
	}
	public void ResumeGame()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void PauseGame()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void PlayGame()
	{
		SceneManager.LoadScene("UltraStellarTest");
		theScoreManager.gameObject.SetActive(true);
		theWaveSpawner.gameObject.SetActive(true);
	}

	public void LoadMainMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
		theScoreManager.gameObject.SetActive(false);
		theWaveSpawner.gameObject.SetActive(false);
	}

	public void QuitGame()
	{
		Debug.Log("Quitting Game");
		GameManager.instance = null;
		Application.Quit();
	}

	private void SpawnPlayer()
	{
		//Spawn the next player at the player at the player start position
		currentPlayer = Instantiate(thePlayerPrefab, playerStartPoint, transform.rotation);
		//set the player start point to the current player position
		playerStartPoint = currentPlayer.transform.position;
		//get the current players health manager and subscribe to the onplayer death event then listen for the player death function call
		currentPlayer.GetComponent<HealthManager>().OnPlayerDeath += PlayerDeath;
		//set the player spawned bool to true
		currentPlayer.GetComponent<HealthManager>().playerIsDead = false;
		playerSpawned = true;
		//check if the player has not spawned and if it hasnt then
		if (OnPlayerSpawned != null)
		{
			//subscribe to the the onplayerspawned event
			OnPlayerSpawned(currentPlayer.GetComponent<PlayerController>());
			Instantiate(respawnEffect,playerStartPoint, transform.rotation);
		}
	}

	public void RestartGame()
	{
		StartCoroutine("RestartGameCo");
	}


	//Restart the game
	public IEnumerator RestartGameCo()
	{
		//When restarting

		//set the score increasing bool to false
		theScoreManager.scoreIncreasing = false;
		//set the player spawned bool to false
		playerSpawned = false;
		//wait for x seconds before restarting
		yield return new WaitForSeconds(0.5f);
		//Once Restarted

		//respawn the player
		SpawnPlayer();

		//set the wave spawner state to counting to respawn enemies 
		//theWaveSpawner.state = SpawnState.COUNTING;

		//set the current player position to the player start point
		currentPlayer.transform.position = playerStartPoint;

		//TODO: Enemy spawn reset
		//reset the enemy spawn points (not working)
		//theEnemySpawnPoints[0].position = enemySpawnerStartPos[0];

		//set the current score to 0
		theScoreManager.scoreCount = 0;
		//set the score increasing bool to true
		theScoreManager.scoreIncreasing = true;
	}

}



