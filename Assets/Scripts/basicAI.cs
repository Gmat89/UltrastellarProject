using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicAI : MonoBehaviour
{
	public GameObject[] waypoints;
	private int current = 0;
	public float rotSpeed;
	public float speed;
	private float WPradius = 1;
	public GameObject pickUp;
	public ParticleSystem popEffect;

	void Start()
	{
	
	}

	void Update()
	{
		transform.Rotate(0,rotSpeed,0);
		if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
		{
			current = Random.Range(0, waypoints.Length);
			if (current >= waypoints.Length)
			{
				current = 0;
			}
		}
		transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position,
			Time.deltaTime * speed);

	}

	public void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			Debug.Log("Hit Asteroid");
			gameObject.SetActive(false);
			Instantiate(popEffect, transform.position, transform.rotation);
			popEffect.Clear(true);
			//Destroy(gameObject);
			Instantiate(pickUp, transform.position, transform.rotation);
		}
	}

}

