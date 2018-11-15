using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
	//the gameobject to spawn, will be changed to an array eventually as more pickups are spawned
	public GameObject pickupObject;
	//the initial count which will increase as pickups are spawned
	private int Count = 0;
	//Maximum number of pickups to spawn
	public int maxCount = 12;
	//Time between each spawn
	public float timeBetweenSpawns = 2.0f;
	//Bool to check if the pickup has spawned
	public bool pickupSpawned;

	void Start()
	{
		//set the bool to false as the game starts
		pickupSpawned = false;
		//Spawn the pickup repeatedly based on the time between spawn value
		InvokeRepeating("Spawn", 0, timeBetweenSpawns);
	}

	void Spawn()
	{
		//Spawn the pickup at the relative location 
		Instantiate(pickupObject, transform.position, transform.rotation);
		//Set the bool to true once the pickup has spawned
		pickupSpawned = true;
		//Add to the count as the pickup has spawned
		Count++;
		//If the current count is bigger than the maxCount then cancel spawning the pickups
		if (Count >= maxCount)
		{
			//Cancel the invoke method to stop the spawner based on the count/maxcount variable
			CancelInvoke("Spawn");
		}
	}
}