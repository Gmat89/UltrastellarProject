using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamsCrapUI : MonoBehaviour
{
	public Text textComponent;
	PlayerController findObjectOfType;
	PlayerHealthManager playerHealthManager;

	// Use this for initialization
	void Start()
	{
		// Look up the play
		findObjectOfType = FindObjectOfType<PlayerController>();

		playerHealthManager = findObjectOfType.GetComponent<PlayerHealthManager>();

		playerHealthManager.OnHealthChanged += OnHealthChanged;
	}

	private void OnHealthChanged(float amount)
	{
		textComponent.text = playerHealthManager.currentHealth.ToString();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
