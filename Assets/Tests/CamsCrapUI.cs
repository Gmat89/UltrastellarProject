using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamsCrapUI : MonoBehaviour
{
	public Text textComponent;
	PlayerController findObjectOfType;
	HealthManager theHealthManager;

	// Use this for initialization
	void Start()
	{
		// Look up the play
		findObjectOfType = FindObjectOfType<PlayerController>();

		theHealthManager = findObjectOfType.GetComponent<HealthManager>();

		theHealthManager.OnHealthChanged += OnHealthChanged;
	}

	private void OnHealthChanged(float amount)
	{
		textComponent.text = theHealthManager.currentHealth.ToString();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
