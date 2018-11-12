using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject thePlayer;

	private ScoreManager theScoreManager;
	

	public bool gameIsRunning;

	public static GameManager instance;

	private WaveSpawner theWaveSpawner;

	public void PlayGame()
	{
		Debug.Log("Game has Started");
		//theEventManager.StartGame();
	}
	void Awake()
	{
		// If an instance already exists, destroy this one
		if (instance != null)
			Destroy(this.gameObject);
		// Otherwise, make this the instance
		else
			instance = this;

		// Enable persistence across scenes
		DontDestroyOnLoad(this.gameObject);
	}
	// Use this for initialization
	void Start ()
	{
		gameIsRunning = true;
		theScoreManager = FindObjectOfType<ScoreManager>();
		//theWaveSpawner = FindObjectOfType<WaveSpawner>();



	}


	
	// Update is called once per frame
	void Update ()
	{
		
	}


	public void RestartGame()
	{
		StartCoroutine("RestartGameCo");
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
