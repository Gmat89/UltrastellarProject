using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
	public bool IsAttacking;

	public BulletController bullet;
	public float bulletSpeed;

	//public float timeBetweenShots;
	//public float shotCounter;

	public Transform bulletSpawnPoint;


	public Transform thePlayer;
	public Transform theEnemy;
	
	public Vector3 Distance;
	public float DistanceFrom;
	public float DistanceToPlayer = 20;

	public float Timer = 2;
	
	//public Rigidbody theBulletRB;
	//public float fireRate = 0.5f;
	//public float nextFire = 0;

	void Update()
	{
		EnemyAttacking();

		// Calculate the distance between the player  the enemy

		Distance = (theEnemy.position - thePlayer.position);
		Distance.y = 0;
		DistanceFrom = Distance.magnitude;
		Distance /= DistanceFrom;

		// If the player is 20m away from the enemy, ATTACK!

		if (DistanceFrom < DistanceToPlayer)
		{
			IsAttacking = true;
		}
		// If the player is not 20m away from the enemy, don't attack
		else
		{
			IsAttacking = false;
		}
	}

	void EnemyAttacking()
	{
			if (IsAttacking)
			{
				Timer -= 1 * Time.deltaTime;
				if(Timer <= 0)
				{
					//Set the shot counter to the value of the time it takes between each shot
					//shotCounter = timeBetweenShots;
					//Spawn the bullet at the attached/specified bullet spawn point
					BulletController newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
					//The spawned bullet speed is equal to the defined bulletSpeed variable.
					newBullet.speed = bulletSpeed;
					Timer = 2f;
				}
			}



			/*if (Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;

				Instantiate(theBullet, theBulletSpawnPoint.position, theBulletSpawnPoint.rotation);
				theBullet.AddForce(theBulletSpawnPoint.forward * 5000);
			}*/
		}
}


