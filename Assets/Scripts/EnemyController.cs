using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	private Rigidbody myRB;
	public float moveSpeed;

	public PlayerController thePlayer;

	// Use this for initialization
	void Start ()
	{
		myRB = GetComponent<Rigidbody>();
		thePlayer = FindObjectOfType<PlayerController>();
	}

	void FixedUpdate()
	{
		//move at a set speed
		myRB.velocity = (transform.forward * moveSpeed);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//search for the player and look at them whereever the move
		transform.LookAt(thePlayer.transform.position);
	}


}
