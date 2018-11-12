using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    public AudioSource PlayerDeath;
	public float maxHealth;
	public float currentHealth;
	public bool playerIsDead;

	public event Action<float> OnHealthChanged;
	public event Action<float> OnHit;
	public GameUI theUIManager;

	//Add Public AudioSource here

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
				//set the player is dead bool to true
				playerIsDead = true;
				//check if the player is dead bool is true then..
				if (playerIsDead)
				{
					//sets the player game object visibility to false.
					gameObject.SetActive(false);
                    //Audio code here for when the player dies
                    PlayerDeath.Play();
				}
			}
		}
		//if (OnHit != null)
		//{
		//	theUIManager.UpdateHealthBar(currentHealth);
		//}
	}
	
	// Use this for initialization
	void Start ()
	{
		playerIsDead = false;
		currentHealth = maxHealth;
	}
	
	public void DamagePlayer(float damageAmount)
	{
		//take damage amount away from player health when it's hit based on the change event.
		Change(-damageAmount);
	}
}
