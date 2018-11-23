using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AudioSource EnemyDestroyed;
    public BoxCollider EnemyCollider;
    public Destroy DestroyScript;

	private Rigidbody myRB;
	public float moveSpeed;

	public PlayerController thePlayer;

	public float flashLength;
	public float flashCounter;

	private Renderer rend;
	private Color storedColor;
	public ParticleSystem deathEffect;
	public HealthManager theHealthManager;

	// Use this for initialization
	void Start()
	{
		//Find this objects renderer
		rend = GetComponent<Renderer>();
		//Create a stored colour value on the renderer
		storedColor = rend.material.GetColor("_Color");
		//Find this objects rigidbody
		myRB = GetComponent<Rigidbody>();
		//Find the player game Object in the game scene
		thePlayer = FindObjectOfType<PlayerController>();
		//Find the HealthManager and subscribe to an event based on the health variable
		GetComponent<HealthManager>().OnHealthChanged += DamageEffect;
        DestroyScript = FindObjectOfType<Destroy>();
        EnemyCollider = GetComponent<BoxCollider>();
	}

	void FixedUpdate()
	{
		//move at a set speed
		myRB.velocity = (transform.forward * moveSpeed);
	}

	// Update is called once per frame
	void Update()
	{
		//search for the player and look at them whereever the move
		transform.LookAt(thePlayer.transform.position);
		if (flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
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
		if (theHealthManager.currentHealth <= 0)
		{
            EnemyCollider.enabled = !EnemyCollider.enabled;
            EnemyDestroyed.Play();
			//Spawns the death effect at the relative location
			Instantiate(deathEffect, transform.position, transform.rotation);
            //hide the death effect
            //gameObject.SetActive(false);
            //Destory the particle effect
            //Destroy(deathEffect);
            DestroyScript.OnDestroy();
		}
	}
}





