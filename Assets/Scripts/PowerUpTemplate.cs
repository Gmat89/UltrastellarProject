using System.Collections;
using UnityEngine;

public class PowerUpTemplate : MonoBehaviour
{
	//Reference to the gun controller
	public GunController theGunController;
	//Rate of Fire Power reference
	public float ROFPower;
	//The object to pickup
	public GameObject pickupEffect;
	//The particle effect once the pickup has been collected
	public ParticleSystem pickupParticle;
	//The duration of the powerup
	public float powerupDuration = 4.0f;
    public AudioSource PickupAudio;
    public AudioClip AsteroidExplosion;



	// Use this for initialization
	void Start()
	{
		//Declare the gunController, Find the object in the scene
		theGunController = FindObjectOfType<GunController>();
		//Declare the pickup Particle effect, find the particle system
		pickupParticle = GetComponent<ParticleSystem>();
        //pickup Audio
        PickupAudio = GetComponent<AudioSource>();
        // Explosion Audio
        AsteroidExplosion = GetComponent<AudioClip>();
        
	}

	// Update is called once per frame
	void Update()
	{
		

	}


	public void OnTriggerEnter(Collider other)
	{
		//If the object that has been collided with is the Player
		if (other.gameObject.tag == "Player")
		{
			//Clear the particle effect upon collision once its finished playing
			pickupParticle.Clear();
			//Start the coroutine for the pickup
			StartCoroutine(Pickup());
            //Play Pickup Sound
            PickupAudio.Play();

        }
	}

	IEnumerator Pickup()
	{
		//Spawn Effect when pick is collected
		Instantiate(pickupEffect, transform.position, transform.rotation);
		
		//Apply the powerup to the Player
		theGunController.timeBetweenShots -= ROFPower;
		//Calculation to clamp the timebetween shots variable so that it never exceeds a set amount in the negative
		if (theGunController.timeBetweenShots < 0.1)
		{
			theGunController.timeBetweenShots = Mathf.Clamp(0, 0, theGunController.timeBetweenShots);
		}
		//Calculation to clamp the timebetween shots variable so that it never exceeds a set amount in the positive
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

	