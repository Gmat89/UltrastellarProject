using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// OnPlayerSpawn from the player controller class
	public event Action<PlayerController> OnPlayerSpawned;
	// OnPlayerdeath from the player controller class
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

	//Reference to the WaveSpawner Spawn Points
	//public Transform[] theEnemySpawnPoints;
	//public Vector3[] enemySpawnerStartPos;
	//Current player in the scene
	public GameObject currentPlayer;

	//public int spawnPosAmount;
	//Set the current controller to the one in the scene
	public void SetPlayerController(PlayerController c)
	{
		controller = c;
	}



	public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

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
	}

	private void PlayerDeath(float obj)
	{
		//Unsub from their ondeath (or you'll get nulls)
//		currentPlayer.GetComponent<HealthManager>().OnPlayerDeath -= PlayerDeath;
		Destroy(currentPlayer);
		if (OnPlayerHasDied != null) OnPlayerHasDied(currentPlayer.GetComponent<PlayerController>());
		RestartGame();

	}

	void Update()
	{

	}

	private void SpawnPlayer()
	{
		//Spawn the player at the players start position
		currentPlayer = Instantiate(thePlayerPrefab, playerStartPoint, transform.rotation);
		//set the player start position to the current player spawn point
		playerStartPoint = currentPlayer.transform.position;
		//subscribe to the current players health manager and run the player death function when needed
		currentPlayer.GetComponent<HealthManager>().OnPlayerDeath += PlayerDeath;
		//set the current players health manager bool player is dead to false
		currentPlayer.GetComponent<HealthManager>().playerIsDead = false;
		//set player spawned bool to true
		playerSpawned = true;
		//check if the player has spawned event is not null
		if (OnPlayerSpawned != null)
		{
			//run the player spawned event in relation to the current player/player controller.
			OnPlayerSpawned(currentPlayer.GetComponent<PlayerController>());
		}
	}
	//call this to restart the game
	public void RestartGame()
	{
		//restart the game using the coroutine
		StartCoroutine("RestartGameCo");
	}

	//Quit function
	public void OnApplicationQuit()
	{
		//set the game manager to null
		GameManager.instance = null;
		//HACK: 
		//Quit gamr code here
	}



	//Restart the game
	public IEnumerator RestartGameCo()
	{
		//When restarting

		//set score increasing bool to false
		theScoreManager.scoreIncreasing = false;
		//set spawned player bool to false
		playerSpawned = false;
		//wait for 0.5f then restart the game
		yield return new WaitForSeconds(0.5f);
		//Once Restarted

		//Spawn the player
		SpawnPlayer();
		//set the wave spawner state to counting which will spawn more enemies
		theWaveSpawner.state = SpawnState.COUNTING;
		//set the spawned player start position to the start point transform
		currentPlayer.transform.position = playerStartPoint;

		//theEnemySpawnPoints[0].position = enemySpawnerStartPos[0];
		//thePlayerPrefab.gameObject.SetActive(true);
		//set the current score to 0
		theScoreManager.scoreCount = 0;
		//restart the score increasing bool
		theScoreManager.scoreIncreasing = true;
	}


	public void PauseGame()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			StartCoroutine("Pause");
		}

	}
	//Pause game coroutine
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
