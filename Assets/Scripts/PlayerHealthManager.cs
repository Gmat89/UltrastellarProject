using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{

	public float startingHealth;
	public float currentHealth;
	public GameObject healthBar;

	public float flashLength;
	private float flashCounter;

	private Renderer rend;
	private Color storedColor;

	public bool playerIsDead;

	public ParticleSystem playerDeathEffect;


	// Use this for initialization
	void Start ()
	{
		healthBar = GameObject.Find("HealthbarG");

		currentHealth = startingHealth;
		rend = GetComponent<Renderer>();
		storedColor = rend.material.GetColor("_Color");
		playerIsDead = false;
		//InvokeRepeating("decreasehealth",1f,1f);

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
				//calls the player death function
				PlayerDeath();
			}
		}
		float calc_Health = currentHealth / startingHealth; //if current health is 80 / 100 = 0.8f
		SetHealthBar(calc_Health);
		if (flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0)
			{
				rend.material.SetColor("_Color", storedColor);
			}
		}
		
	}

//	void decreasehealth()
	//{
		//currentHealth -= 10; // Constant Damage Tester
	//}

	public void HurtPlayer(float damageAmount)
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
		//Load the gameover scene
		SceneManager.LoadScene("GameOver");
	}

	public void SetHealthBar(float myHealth)
	{
		//myHealth needs to be a value between zero and one
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0,1), healthBar.transform.localScale.y,
			healthBar.transform.localScale.z);

	}
}
