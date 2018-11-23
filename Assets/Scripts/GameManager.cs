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

	//Reference to the WaveSpawner Spawn Points
	//public Transform[] theEnemySpawnPoints;
	//public Vector3[] enemySpawnerStartPos;

	public GameObject currentPlayer;
	public int spawnPosAmount;

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
		playerStartPoint = currentPlayer.transform.position;
		currentPlayer.GetComponent<HealthManager>().OnPlayerDeath += PlayerDeath;
		//set the player spawned bool to true
		currentPlayer.GetComponent<HealthManager>().playerIsDead = false;
		playerSpawned = true;

		if (OnPlayerSpawned != null)
		{
			OnPlayerSpawned(currentPlayer.GetComponent<PlayerController>());
		}
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
		//When restarting
		theScoreManager.scoreIncreasing = false;
		playerSpawned = false;
		yield return new WaitForSeconds(0.5f);
		//Once Restarted
		SpawnPlayer();
		
		//theUIManager.thePlayerHealthBar = GameObject.FindGameObjectWithTag("Healthbar");
		theWaveSpawner.state = SpawnState.COUNTING;
		currentPlayer.transform.position = playerStartPoint;
		//theEnemySpawnPoints[0].position = enemySpawnerStartPos[0];
		//thePlayerPrefab.gameObject.SetActive(true);
		theScoreManager.scoreCount = 0;
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
