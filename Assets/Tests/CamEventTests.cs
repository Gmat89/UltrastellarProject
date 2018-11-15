using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEventTests : MonoBehaviour
{
	// I'm going to be messing with particles to make a crap effect
	public ParticleSystem particles;

	// Subscribe to events here. Just once
	void Start()
	{
		// Yes, you can put loads of shit on one line.
		// Here I look up the playerController on the player, THEN get the health component inside that,
		// THEN find the event in the health and add my function to the 'listeners'
		FindObjectOfType<PlayerController>().GetComponent<HealthManager>().OnHealthChanged += OnHealthChanged;
	}

	// This function gets called when the event gets called
	private void OnHealthChanged(float obj)
	{
		print("CamTester: A ship health changed");
		particles.Emit(1000);
	}

	// Now I don't need to fuck about constantly reading other objects every frame to see what they're doing
	void Update()
	{

	}
}
