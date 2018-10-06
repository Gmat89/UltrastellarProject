using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	//Note: this script can be used on different types of guns by changing the variables
	public bool isFiring;

	public BulletController bullet;
	public float bulletSpeed;

	public float timeBetweenShots;
	private float shotCounter;

	public Transform bulletSpawnPoint;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if the gun is firing
		if (isFiring)
		{
			shotCounter -= Time.deltaTime;
			if (shotCounter <= 0)
			{
				shotCounter = timeBetweenShots;
				BulletController newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as BulletController;
				newBullet.speed = bulletSpeed;
			}
			else
			{
				shotCounter = 0;
			}
		}
	}
}
