using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerHealthManager : MonoBehaviour
{

	public float startingHealth;
	public float currentHealth;
	public GameObject healthBar;
	public bool playerIsDead;
	int amount;

	private EventManager theEventManager;
	public event Action<int> OnHealthChanged;
	public event Action<int> OnDeath;

	public void Change(int changeAmount)
	{
		amount += changeAmount;
		if (OnHealthChanged != null)
		{
			OnDeath(changeAmount);
		}
	}


	// Use this for initialization
	void Start ()
	{
		healthBar = GameObject.Find("HealthbarG");
		playerIsDead = false;
		currentHealth = startingHealth;
		//InvokeRepeating("decreasehealth",1f,1f);

	}
	
	// Update is called once per frame
	void Update ()
	{
		float calc_Health = currentHealth / startingHealth; //if current health is 80 / 100 = 0.8f
		SetHealthBar(calc_Health);
	}
	void OnEnable()
	{
		if (currentHealth <= 0)
		{
			theEventManager.OnPlayerDeath += OnPlayerDeath;
		}
	}

	void OnDisable()
	{
		if (playerIsDead)
		{
			theEventManager.OnPlayerDeath -= OnPlayerDeath;
		}
	}
		

	public void DamagePlayer(float damageAmount)
	{
		//take damage amount away from player health when it's hit.
		currentHealth -= damageAmount;
	}




	void OnPlayerDeath()
	{
		if (currentHealth <= 0)
		{
			playerIsDead = true;
			if (playerIsDead)
			{
				//sets the player game object visibility to false.
				gameObject.SetActive(false);
				//spawns the particle effect
				//Instantiate(playerDeathEffect, transform.position, transform.rotation);
				//sets the particle effect visibility to false once it's finished playing
				//gameObject.SetActive(false);
				//Load the gameover scene
				//SceneManager.LoadScene("GameOver");
			}
		}
	}

	public void SetHealthBar(float myHealth)
	{
		//myHealth needs to be a value between zero and one
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0,1), healthBar.transform.localScale.y,
			healthBar.transform.localScale.z);

	}
}
