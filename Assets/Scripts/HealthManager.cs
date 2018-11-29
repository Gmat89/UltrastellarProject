using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
	//audio source reference
    public AudioSource deathSound;
	//the maximum health
	public float maxHealth;
	//the current health
	public float currentHealth;
	//check if the player is dead
	public bool playerIsDead;
	//event for when the health is changed
	public event Action<float> OnHealthChanged;
	//event for when the player dies
	public event Action<float> OnPlayerDeath;
	//event for when the enemy dies
	public event Action<float> OnEnemyDeath;
	//check if the enemy is dead
	public bool enemyIsDead;
	

	//Starts an event when the health variable is changed
	public void Change(float changeAmount)
	{
		//update the current health to the changed amount(damage)
		currentHealth += changeAmount;
		//check if the onhealthchanged event is not null
		if (OnHealthChanged != null)
		{
			//run the onhealthchanged event
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
						//run the onplayerdeath event
						OnPlayerDeath(0);
					}
					
				}
				//check if the enemy is dead bool is true then 
				else if (enemyIsDead)
				{
					//check if the on enemy death event is not null
					if (OnEnemyDeath != null)
					{
						// run the onenemydeath event
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
		//take damage amount away from current health when it's hit based on the change event.
		Change(-damageAmount);
	}
	
}
