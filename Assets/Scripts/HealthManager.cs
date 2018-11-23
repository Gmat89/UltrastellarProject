using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    public AudioSource deathSound;
	public float maxHealth;
	public float currentHealth;
	public bool playerIsDead;
	public event Action<float> OnHealthChanged;
	public event Action<float> OnPlayerDeath;
	public event Action<float> OnEnemyDeath;
	
	public bool enemyIsDead;
	public bool particleSpawned;

	//Starts an event when the health variable is changed
	public void Change(float changeAmount)
	{
		currentHealth += changeAmount;
		if (OnHealthChanged != null)
		{
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
