using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
	//Maximum health variable
	public float maxHealth;
	//Current health variable
	public float currentHealth;
	//Bool to check when the player is dead
	public bool playerIsDead;
	//Event for when the health variable changes
	public event Action<float> OnHealthChanged;
	//Event for when the player dies
	public event Action<float> OnPlayerDeath;
	//Event for when the enemy dies
	public event Action<float> OnEnemyDeath;
	//Bool to check when the enemy is dead
	public bool enemyIsDead;
	//Bool to check with a particle has spawned
	public bool particleSpawned;

	//Starts an event when the health variable is changed
	public void Change(float changeAmount)
	{
		//set the current health to the change amount
		currentHealth += changeAmount;
		//Check if the current health variable changed is not null
		if (OnHealthChanged != null)
		{
			//Run the health changed event
			OnHealthChanged(changeAmount);
			//If the current health is <= 0 then...
			if (currentHealth <= 0)
			{
				//if the objects tag equals enemy then..
				if(gameObject.tag == "Enemy")
					//set the enemy is dead bool to true
					enemyIsDead = true;

				//if the objects tag equals player then...
				if (gameObject.tag == "Player")
				{
					//set the player is dead bool to true
					playerIsDead = true;
				}
			
				
				//check if the player is dead bool is true then..
				if (playerIsDead)
				{
					// if the player death event is not null then run it
					if (OnPlayerDeath != null)
					{
						OnPlayerDeath(0);
					}
					
				}
				//check if the enemy is dead bool is true then 
				else if (enemyIsDead)
				{
					//check if the on enemy death event is not null
					if (OnEnemyDeath != null)
					{
						// run the event
						OnEnemyDeath(0);
					}
				}
			}
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		//set enemy is dead bool to false
		enemyIsDead = false;
		//set playe is dead bool to false
		playerIsDead = false;
		//Set the current health to the Max health value
		currentHealth = maxHealth;
	}
	
	public void DamageObject(float damageAmount)
	{
		//take damage amount away from player health when it's hit based on the change event.
		Change(-damageAmount);
	}
	
}
