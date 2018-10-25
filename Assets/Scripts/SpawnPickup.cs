using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
	public GameObject pickupObject;
	private int enemyCount = 0;
	public int maxEnemyCount = 12;
	public float timeBetweenSpawns = 2.0f;
	public bool pickupSpawned;

	void Start()
	{
		pickupSpawned = false;
		InvokeRepeating("Spawn", 0, timeBetweenSpawns);
	}

	void Spawn()
	{

		Instantiate(pickupObject, transform.position, transform.rotation);
		pickupSpawned = true;
		enemyCount++;
		if (enemyCount >= maxEnemyCount)
		{
			CancelInvoke("Spawn");
		}
	}
}