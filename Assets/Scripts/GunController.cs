using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	//Note: this script can be used on different types of guns by changing the variables
	public bool isFiring;

	public BulletController bullet;
	public float bulletSpeed;

	public float timeBetweenShots = 0.3333f;
	public float shotCounter;
	private float timestamp;

	public Transform bulletSpawnPoint;

	// Use this for initialization
	void Start ()
	{
		isFiring = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if the gun is firing
		if (isFiring)

		{
			if (Time.time >= timestamp && (Input.GetMouseButton(0)))
			{
				isFiring = true;
				BulletController newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
				timestamp = Time.time + timeBetweenShots;
				newBullet.speed = bulletSpeed;
			}
		}

	
	}
}

