using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Another crap example of my reacting to OTHER object's events.
/// WITHOUT polluting their code with my hacked shit
/// </summary>
public class CamsCrapUI : MonoBehaviour
{
	public Text textComponent;
	PlayerController findObjectOfType;
	HealthManager healthManager;

	// Use this for initialization
	void Start()
	{
		// This does exactly the same job as the "CamEventTests" single liner, but here I'm storing each lookup in their own variables, so I don't have to look them up again later
		findObjectOfType = FindObjectOfType<PlayerController>();

		healthManager = findObjectOfType.GetComponent<HealthManager>();

		healthManager.OnHealthChanged += OnHealthChanged;
	}

	private void OnHealthChanged(float amount)
	{
		textComponent.text = healthManager.currentHealth.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		// Don't need any inefficient, annoying, constant update code!
	}
}
