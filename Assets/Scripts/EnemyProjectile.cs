using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
	//bool to check if the enemy is attacking
	public bool IsAttacking;

	//Reference to the bullet
	public EnemyBulletController bullet;
	
	//Bullet speed
	public float bulletSpeed;

	//Bullet spawn point
	public Transform bulletSpawnPoint;

	//Players transform
	public Transform thePlayer;

	//Enemy transform
	public Transform theEnemy;

	//Distance between the player and enemy
	public Vector3 Distance;

	//distance from the player
	public float DistanceFrom;

	//Distance to the player (Radius)
	public float DistanceToPlayer = 20;

	//Timer for bullet spawn
	public float Timer = 2;

	void Update()
	{

		//Run enemy attacking function
		EnemyAttacking();

		// Calculate the distance between the player and the enemy

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
			if (Timer <= 0)
			{
				//Spawn the bullet at the attached/specified bullet spawn point
				EnemyBulletController newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
				//The spawned bullet speed is equal to the defined bulletSpeed variable.
				newBullet.speed = bulletSpeed;
				Timer = 2f;
			}
		}
	}
}




