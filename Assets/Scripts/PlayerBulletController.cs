using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
	public float speed;
	public float lifeTime;
	public int damageToGive;
	//this is to test source control
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Add velocity to the bullets transform based on the defined speed multiplied by Time
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		lifeTime -= Time.deltaTime;
		//if the lifetime of the bullet equals 0 then destroy the bullet.
		if (lifeTime <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Damage the enemy on collision
			other.gameObject.GetComponent<HealthManager>().DamageObject(damageToGive);
			//Destroy the bullet object
			Destroy(gameObject);
		}
	}
}
