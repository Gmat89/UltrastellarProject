using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEventTests : MonoBehaviour
{
	public ParticleSystem particles;

	// Use this for initialization
	void Start()
	{
		FindObjectOfType<PlayerController>().GetComponent<HealthManager>().OnHealthChanged += OnHealthChanged;
	}

	private void OnHealthChanged(float obj)
	{
		print("CamTester: A ship health changed");
		particles.Emit(1000);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
