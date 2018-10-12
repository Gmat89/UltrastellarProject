using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

	public int startingHealth;
	public int currentHealth;

	public float flashLength;
	private float flashCounter;

	private Renderer rend;
	private Color storedColor;

	public bool playerIsDead;

	public ParticleSystem playerDeathEffect;


	// Use this for initialization
	void Start ()
	{
		currentHealth = startingHealth;
		rend = GetComponent<Renderer>();
		storedColor = rend.material.GetColor("_Color");
		playerIsDead = false;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentHealth <= 0)
		{
			playerIsDead = true;
			if (playerIsDead)
			{
				//sets the player game object visibility to false.
				gameObject.SetActive(false);
				PlayerDeath();
			}
		}
		if (flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0)
			{
				rend.material.SetColor("_Color", storedColor);
			}
		}
	}

	public void HurtPlayer(int damageAmount)
	{
		//take damage amount away from player health when it's hit.
		currentHealth -= damageAmount;
		//Change the colour of the player when they are hit
		flashCounter = flashLength;
		//Sets the renderers flash colour from the stored colour
		rend.material.SetColor("_Color", Color.red);
	}


	public void PlayerDeath()
	{
		//spawns the particle effect
		Instantiate(playerDeathEffect, transform.position, transform.rotation);
		//sets the particle effect visibility to false once it's finished playing
		gameObject.SetActive(false);
	}
}
