using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class EnemyController : MonoBehaviour
{
	//enemies rigidbody
	private Rigidbody myRB;
	//enemy move speed
	public float moveSpeed;
	//reference to the player controller
	public PlayerController thePlayer;
	//reference to the player game object
	public GameObject thePlayerObject;

	//damage flash length
	public float flashLength;
	//damage flash counter
	public float flashCounter;
	//enemy renderer
	public Renderer rend;
	//flash colour
	private Color storedColor;
	//death particle effect
	public ParticleSystem deathEffect;
	public ParticleSystem spawn;
	//reference to the health manager
	public HealthManager theHealthManager;
	//reference to the score  manager
	private ScoreManager theScoreManager;
	//score to give when killed
	public int scoreToGive;

	public Transform myTransform;
	public float rotationSpeed;
	public bool isChasing;
	public float maxDist;
	public float minDist;
	public float fireDist;

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
		Instantiate(spawn, transform.position, transform.rotation);

	}

	void FixedUpdate()
	{
		//move at a set speed
		//myRB.velocity = (transform.forward * moveSpeed);
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		

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
			float distance = (thePlayer.transform.position - myTransform.position).magnitude;
			isChasing = true;
			if (isChasing)
			{
				//search for the player and look at them whereever the move
				myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(thePlayer.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
				//transform.LookAt(thePlayer.transform.position);
			}
			//Moves too far away from player
			if (distance > maxDist)
			{
				isChasing = false;
			}
			if (distance < minDist)
			{
				myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(myTransform.position - thePlayer.transform.position), rotationSpeed * Time.deltaTime);
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			}
			else
			{
				if (distance < maxDist)
				{
					isChasing = true;
				}
			}

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
		rend.material.SetColor("_Color", Color.red);
		//Check if the current health of this object is <= 0 then....
		
	}

	public void EnemyDeath(float amount)
	{
		//Check if the enemy is dead bool is true in the HealthManager
		if (theHealthManager.currentHealth <= 0)
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





