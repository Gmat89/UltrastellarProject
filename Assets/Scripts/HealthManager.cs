using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    
	public float maxHealth;
	public float currentHealth;
	public bool playerIsDead;

	public event Action<float> OnHealthChanged;
	public UIManager theUIManager;

	public bool enemyIsDead;
	public bool particleSpawned;
    public AudioSource deathSoundSource;
    public AudioClip deathClip;

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
				DeathSound();
                //set the player is dead bool to true
				playerIsDead = true;
				enemyIsDead = true;
				//check if the player is dead bool is true then..
				if (playerIsDead)
				{
                    
                    
                    //sets the player game object visibility to false.
                   //gameObject.SetActive(false);
                    //Audio code here for when the player dies

                }
				else if (enemyIsDead)
				{
					gameObject.SetActive(false);					
				}
			}
		}
		//if (OnHit != null)
		//{
		//	theUIManager.UpdateHealthBar(currentHealth);
		//}
	}
    public void DeathSound()
    {
        deathSoundSource.PlayOneShot(deathClip);
    }
    // Use this for initialization
    void Start ()
	{
		enemyIsDead = false;
		playerIsDead = false;
		currentHealth = maxHealth;
        deathSoundSource = GetComponent<AudioSource>();

    }
	
	public void DamageObject(float damageAmount)
	{
		//take damage amount away from player health when it's hit based on the change event.
		Change(-damageAmount);
	}

	
}
