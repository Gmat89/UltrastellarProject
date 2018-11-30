using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//Spawn states definition
public enum SpawnState { SPAWNING, WAITING, COUNTING }

public class WaveSpawner : MonoBehaviour
{
    public AudioSource EnemySpawn;
	//Allows us to change the values of the instances of this class inside Unity Inspector
	[System.Serializable]
	//Class that is stored in an array
	public class Wave
	{
		//Name of the wave(Announce)
		public string name;
		//Prefabs transform to be instantiated
		public Transform[] enemies;
		//The count
		public int count;
		//The spawn rate
		public float rate;
		
	}
	//The Wave class array
	public Wave[] waves;
	//Store the index of the wave to be created
	private int nextWave = 0;
	//Next wave variable get/return
	public int NextWave
	{
		get { return nextWave + 1; }
	}

	//Spawn points array
	public Transform[] spawnPoints;
	//Time between each wave
	public float timeBetweenWaves = 5f;

	//Wave countdown variables get/return
	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}
	//Search game objects for tag 
	private float searchCountdown = 1f;

	//Spawn State variables
	public SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}
	
	void Start()
	{
		//If no spawn points are found/assigned then print this error.
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}
		//Set the waveCountdown to timeBetweenWaves, the countdown is set to 5 seconds
		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{

		if (state == SpawnState.WAITING)
		{
			//Check if the enemies are not still alive
			if (!EnemyIsAlive())
			{
				//Tell the player the wave has been defeated
				Debug.Log("Wave Completed/Defeated!!");
				//Run the wave completed function. Begin a new wave
				WaveCompleted();
				//Stop the spawner from looping
				//return;
			}
			//If some enemies are still alive
			else
			{
				//Then let the player finish them off
				return;
			}
		}
		//Check if the waveCountdown less than or equal to 0 then..(If its time to start spawning a wave)
		if (waveCountdown <= 0)
		{
			//Check if the current spawn state is not set to SPAWNING
			if (state != SpawnState.SPAWNING)
			{
				//Start spawning the wave using coroutine and parameters along with the array information
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		//If we haven't reached zero yet
		else
		{
			//Ensure that the timer counts down with each frame 
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		//Let the player know they have completed the round
		Debug.Log("Wave Completed!");
		//Set the current spawn state to COUNTING
		state = SpawnState.COUNTING;
		//Set the wave countdown value to the the timeBetweenWave variable
		waveCountdown = timeBetweenWaves;
		//If the next wave is bigger than the number of waves we have then we want to
		if (nextWave + 1 > waves.Length - 1)
		{
			//Set the next wave to 0
			nextWave = 0;
			//tell the player that all the waves are completed and that the spawner is looping/Restarting
			Debug.Log("ALL WAVES COMPLETE! Looping...");
		}
		//If we havent reached the final wave yet
		else
		{
			//Increment the nextWave variable by 1 to continue spawning enemies
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		//Set the searchCountdown time to the -amount of time passed in game
		searchCountdown -= Time.deltaTime;
		//If the searchCountdown is less than equal to zero
		if (searchCountdown <= 0f)
		{
			//Set the searchCountdown to 1f once it hits zero.
			searchCountdown = 1f;
			//Check if the tag of the enemy is null then..
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				//No enemies are alive
				return false;
			}
		}
		//Enemies are alive
		return true;
	}

	//Coroutine to spawn a wave
	IEnumerator SpawnWave(Wave _wave)//takes Wave class as parameter and _wave as variable
	{
		//Tell the player the name of the next wave
		Debug.Log("Spawning Wave: " + _wave.name);
		//Set the spawn state to SPAWNING
		state = SpawnState.SPAWNING;

		//Begin loop, this will run the number of enemies that we want to spawn.
		for (int i = 0; i < _wave.count; i++)
		{
			for (int e = 0; e < _wave.enemies.Length; e++)
			{
				//Spawn enemy function
				SpawnEnemy(_wave.enemies[e]);
				//Wait for a set amount of time before spawning the next wave
				yield return new WaitForSeconds(1f / _wave.rate);
			}
			//Wait for a set amount of time before spawning the next wave
			yield return new WaitForSeconds(1f / _wave.rate);
		}
		//Set the state to WAITING while the player defeats all the enemies
		state = SpawnState.WAITING;

		yield break;
	}


	//Spawn Enemy Function, with a transform parameter and a enemy variable
	void SpawnEnemy(Transform _enemy)
	{
		//Print the enemy name
		Debug.Log("Spawning Enemy: " + _enemy.name);

		//Choose a random spawn point from the defined spawn point objects in the inspector
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		//Spawn the enemy at the spawn point position and spawn point rotation
		Instantiate(_enemy, _sp.position, _sp.rotation);
        EnemySpawn.Play();
	}

}