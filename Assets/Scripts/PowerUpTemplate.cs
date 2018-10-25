using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTemplate : MonoBehaviour
{
	public GunController theGunController;
	public float ROFPower;
	public GameObject pickupEffect;
	public ParticleSystem pickupParticle;
	public float powerupDuration = 4.0f;
	



	// Use this for initialization
	void Start()
	{
		theGunController = FindObjectOfType<GunController>();
		pickupParticle = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		

	}


	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			pickupParticle.Clear();
			StartCoroutine(Pickup());
		}
	}

	IEnumerator Pickup()
	{
		//Spawn Effect when pick is collected
		Instantiate(pickupEffect, transform.position, transform.rotation);
		
		//Apply the effect to the Player
		theGunController.timeBetweenShots -= ROFPower;

		if (theGunController.timeBetweenShots < 0.1)
		{
			theGunController.timeBetweenShots = Mathf.Clamp(0, 0, theGunController.timeBetweenShots);
		}
		if (theGunController.timeBetweenShots > 0.6)
		{
			theGunController.timeBetweenShots = Mathf.Clamp(0, 0, theGunController.timeBetweenShots);
		}

		//Disable the mesh renderer when pickup is obtained
		GetComponent<MeshRenderer>().enabled = false;
		//Disable collider when the pickup is obtained.
		GetComponent<Collider>().enabled = false;
		//Destroy the particle
		Destroy(pickupParticle);

		//Wait x amount of seconds
		yield return new WaitForSeconds(powerupDuration);

		//Reverse the effect on the player (reset back to default firing rate)
		theGunController.timeBetweenShots += ROFPower;

		//Destory the Pickup object
		Destroy(gameObject);
	}
}

	