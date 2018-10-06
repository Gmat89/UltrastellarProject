using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
	public int scoreToGive;
	public int health;
	public int currentHealth;

	public float flashLength;
	private float flashCounter;

	private Renderer rend;
	private Color storedColor;

	public ParticleSystem deathEffect;

	public bool enemyIsDead;
	public bool particleSpawned;
	private ScoreManager theScoreManager;



	// Use this for initialization
	void Start()
	{
		enemyIsDead = false;
		particleSpawned = false;
		currentHealth = health;
		rend = GetComponent<Renderer>();
		storedColor = rend.material.GetColor("_Color");
		theScoreManager = FindObjectOfType<ScoreManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0)
			{
				rend.material.SetColor("_Color", storedColor);
			}
		}
		if (currentHealth <= 0)
		{
			//Set enemy is dead bool to true
			enemyIsDead = true;
			//Check if the bool is set to true and if it is then call EnemyDeath function
			if (enemyIsDead)
			{
				EnemyDeath();
			}
		}
	}

	public void DamageEnemy(int damageAmount)
	{
		//take damage amount away from enemy health when it's hit.
		currentHealth -= damageAmount;
		//Change colour of the enemy when it is hit.
		rend.material.SetColor("_Color", Color.white);
	}

	public void EnemyDeath()
	{
		//set particle bool to true
		particleSpawned = true;
		//Spawn the particle effect at the enemys location
		Instantiate(deathEffect, transform.position, transform.rotation);
		//Destory the particle effect
		Destroy(gameObject);
		//Add points to the players score based on scoreToGive defined
		theScoreManager.AddScore(scoreToGive);
	}
}
