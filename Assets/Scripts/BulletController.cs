using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float speed;
	public float lifeTime;
	public int damageToGive;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Damage the enemy on collision
			other.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damageToGive);
			//Destroy the bullet object
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Player")
		{
			//Damage the player on collision
			other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
			//Destroy the bullet object
			Destroy(gameObject);
		}
	}
}
