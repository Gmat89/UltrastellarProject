﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_IncreaseMaxHp : MonoBehaviour
{

    //this can either increase current hp or increase max hp
    //The object to pickup
    public GameObject pickupEffect;
    //The particle effect once the pickup has been collected
    public ParticleSystem pickupParticle;

    private HealthManager healthManager;
    public float healthIncrease;
	public AudioSource PickupAudio;


	// Use this for initialization
	void Start()
    {
		//buffSFX.Play();
		healthManager = FindObjectOfType<HealthManager>();
        Debug.Log(healthManager.maxHealth);
        //Declare the pickup Particle effect, find the particle system
        pickupParticle = GetComponent<ParticleSystem>();
		PickupAudio = GetComponent<AudioSource>();
	}


    public void IncreaseMaxHp()
    {
		//If the object that has been collided with is the Player
		if (gameObject.tag == "Player")
        {
            healthManager.maxHealth += healthIncrease;
			Debug.Log(healthManager.maxHealth);
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //If the object that has been collided with is the Player
        if (other.gameObject.tag == "Player")
        {
            healthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
            //Clear the particle effect upon collision once its finished playing
            pickupParticle.Clear();
			//buffSFX.Play();
			//Start the coroutine for the pickup
			StartCoroutine(Pickup());
			PickupAudio.Play();
		}
    }

    IEnumerator Pickup()
    {
		//Spawn Effect when pick is collected
		Instantiate(pickupEffect, transform.position, transform.rotation);
		
		IncreaseMaxHp();
		//Disable the mesh renderer when pickup is obtained
		GetComponent<SpriteRenderer>().enabled = false;
		//Disable collider when the pickup is obtained.
		GetComponent<Collider>().enabled = false;
        //Destroy the particle
        Destroy(pickupParticle);

        //Wait x amount of seconds
        yield return new WaitForSeconds(1.0f);

		
		//Destory the Pickup object
		Destroy(gameObject);
    }
}
