using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
	//the game object array of waypoints assigned in the scene/inspector
	public GameObject[] waypoints;
	//the current amount of waypoints
	private int current = 0;
	//the rotation speed of the object
	public float rotSpeed;
	//the speed in which it moves
	public float speed;
	//the radius of the waypoint
	private float WPradius = 1;
	//the pickup to spawn upon being destroyed
	public GameObject pickUp;
	//the effect when the object is destroyed
	public ParticleSystem popEffect;
   

    void Start()
	{

 	
	}

    void Update()
	{
		//Rotate the object based on the rotSpeed variable on the Y axis
		transform.Rotate(0,rotSpeed,0);
		//Check if the distance between the current waypoint to the next one based off if its greater than the radius value
		if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
		{
			//set the current direction to randomise by the length of the waypoints, as in how many
			current = Random.Range(0, waypoints.Length);
			//check if the current value is less than the waypoint length
			if (current >= waypoints.Length)
			{
				//set the current value to zero
				current = 0;
			}
         
		}

		//move the objects transform towards the current array of waypoints by each frame times the speed variable
		transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position,
			Time.deltaTime * speed);

	}

	public void OnCollisionEnter(Collision other)
	{
		//Check if the objected that has been collided with has a tag of bullet
		if (other.gameObject.tag == "Bullet")
		{
			//Print if it hit
			Debug.Log("Hit Asteroid");
			//Set the asteroid object visibility to false
			gameObject.SetActive(false);
			//Spawn the particle effect at the relative location
			Instantiate(popEffect, transform.position, transform.rotation);
            //Remove the particle effect
            popEffect.Clear(true);
			//Destroy the asteroid gameObject
			//Destroy(gameObject);
			//Spawn the pickup in its place
			Instantiate(pickUp, transform.position, transform.rotation);


        }
	}

}

