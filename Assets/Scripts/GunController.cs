using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public AudioSource GunSound;
    public AudioClip GunFiring;
	//Note: this script can be used on different types of guns/powerUps by changing the variables
	public bool isFiring;

	public BulletController bullet;
	public float bulletSpeed;
	//Calculates
	public float timeBetweenShots = 0.3333f;
	public float shotCounter;
	//point in time
	private float timestamp;

	public Transform bulletSpawnPoint;

	// Use this for initialization
	void Start ()
	{
		//set the firing bool to false at the beginning of the game
		isFiring = false;
        GunSound = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		//if the gun is firing
		if (isFiring)
		{
			//if the player is pressing and holding the left mouse button down, the game gets the current time, checks if it is less than equal to the timestamp variable then;
			if (Time.time >= timestamp && (Input.GetMouseButton(0)))
			{
                shoot();
				//sets the firing bool to true
				isFiring = true;
				
			}
		}
	
	}
    public void ContinueAudio()
    {
        GunSound.PlayOneShot(GunFiring);    
    }
    public void shoot()
    {
        //Spawns the bullet at the location of the Bullet Spawn Point. 
        BulletController newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        ContinueAudio();
        //Sets the timestamp to the current game time while adding the timeBetweenShots variable
        timestamp = Time.time + timeBetweenShots;
        //Sets the spawned bullets speed to the user defined bullet speed variable in the editor.
        newBullet.speed = bulletSpeed;

    }


}

