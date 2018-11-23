using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class EnemyController : MonoBehaviour
{

	private Rigidbody myRB;
	public float moveSpeed;

	public PlayerController thePlayer;
	public GameObject thePlayerObject;

	public float flashLength;
	public float flashCounter;

	private Renderer rend;
	private Color storedColor;
	public ParticleSystem deathEffect;
	public HealthManager theHealthManager;
	private ScoreManager theScoreManager;
	public int scoreToGive;

	// Use this for initialization
	void Start()
	{
		//Find this objects renderer
		rend = GetComponent<Renderer>();
		//Create a stored colour value on the renderer
		storedColor = rend.material.GetColor("_Color");
		//Find this objects rigidbody
		myRB = GetComponent<Rigidbody>();
		//Find the HealthManager and subscribe to an event based on the health variable
		GetComponent<HealthManager>().OnHealthChanged += DamageEffect;
		//Find the ScoreManager
		theScoreManager = FindObjectOfType<ScoreManager>();
		//Subscribe to the OnEnemyDeath event
		GetComponent<HealthManager>().OnEnemyDeath += EnemyDeath;

	}

	void FixedUpdate()
	{
		//move at a set speed
		myRB.velocity = (transform.forward * moveSpeed);
	}

	// Update is called once per frame
	void Update()
	{
		// HACK:
		//Find the player game Object in the game scene
		thePlayer = FindObjectOfType<PlayerController>();


		//Check if the playerisdead bool is false and if the player is in the scene
		if (theHealthManager.playerIsDead == false && thePlayer != null)
		{
			//search for the player and look at them whereever the move
			transform.LookAt(thePlayer.transform.position);
		}
		//Check if the playerisdead bool is true and there is no player in the scene
		if (theHealthManager.playerIsDead == true && thePlayer == null)
		{
			//Find the object with a tag of Player
			thePlayerObject = GameObject.FindGameObjectWithTag("Player");
			//Update the enemy so they continue looking at the player
			transform.LookAt(thePlayer.transform.position);
		}
		//Check if the flash counter is equal to zero
		if (flashCounter > 0)
		{
			//Set the flash counter to -= time
			flashCounter -= Time.deltaTime;
			//Change the colour of the material.
			if (flashCounter <= 0)
			{
				rend.material.SetColor("_Color", storedColor);
			}
		}
	}

	private void DamageEffect(float amount)
	{
		//Set the flash counter value to the amount of the flash length
		flashCounter = flashLength;
		//Sets the renderers flash colour from the stored colour
		rend.material.SetColor("_Color", Color.white);
		//Check if the current health of this object is <= 0 then....
		
	}

	public void EnemyDeath(float amount)
	{
		//Check if the enemy is dead bool is true in the HealthManager
		if (theHealthManager.enemyIsDead)
		{
			//Add the score to the current score
			theScoreManager.AddScore(scoreToGive);
			//Spawns the death effect at the relative location
			Instantiate(deathEffect, transform.position, transform.rotation);
			//hide the death effect
			gameObject.SetActive(false);
			//Destroy the object
			Destroy(gameObject);
		}
	}
}





