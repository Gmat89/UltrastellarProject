using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
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
		//if the lifetime of the bullet equals 0 then destroy the bullet.
		if (lifeTime <= 0)
		{
			Destroy(gameObject);
		}
	}


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			//Damage the player on collision
			other.gameObject.GetComponent<HealthManager>().DamageObject(damageToGive);
			//Destroy the bullet object
			Destroy(gameObject);
		}
	}
}
