using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
	//the gameobject to spawn, will be changed to an array eventually as more pickups are spawned
	public GameObject[] pickupObject;

	//the initial count which will increase as pickups are spawned
	private int Count = 0;

	//Maximum number of pickups to spawn
	public int maxCount = 12;

	//Time between each spawn
	public float timeBetweenSpawns = 2.0f;

	//Bool to check if the pickup has spawned
	public bool pickupSpawned;

	//Bools for checking if a wave has spawned
	public bool waveSpawned;

	//Reference to the wave spawner
	private WaveSpawner theWaveSpawner;

	void Start()
	{
		//set wave has spawned bool to false
		waveSpawned = false;
		//set the bool to false as the game starts
		pickupSpawned = false;
		//Spawn the pickup repeatedly based on the time between spawn value
		InvokeRepeating("Spawn", 0, timeBetweenSpawns);
		//find the wave spawner
		theWaveSpawner = FindObjectOfType<WaveSpawner>();
	}

	void Spawn()
	{
		//check if the wavespawner state is set to SPAWNING and if it is then
		if (theWaveSpawner.State == SpawnState.SPAWNING)
		{
			//set the waveSpawned bool to true
			waveSpawned = true;
		}
		//if the waveSpawned bool is true then spawn the pickups
		if (waveSpawned)
		{
			//Spawn the pickup at the relative location 
			Instantiate(pickupObject[Random.Range(0, pickupObject.Length)], transform.position, transform.rotation);
			//Set the bool to true once the pickup has spawned
			pickupSpawned = true;
			//Add to the count as the pickup has spawned
			Count++;
			//If the current count is bigger than the maxCount and the wave spawner state is == WAITING then 
			if (Count >= maxCount && theWaveSpawner.State == SpawnState.WAITING)
			{
				//Cancel the invoke method to stop the spawner based on the count/maxcount variable
				//CancelInvoke("Spawn");
				//set the waveSpawned bool to false
				waveSpawned = false;
				//set the pickup spawned bool to false
				pickupSpawned = false;
			}
		}
		//else if (theWaveSpawner.State == SpawnState.COUNTING)
		//{
		//	CancelInvoke("Spawn");
		//}
	}
}
