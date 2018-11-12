using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

	public EventManager theEventManager;
	public GameObject healthBar;
	public HealthManager theHealthManager;


	public void Start()
	{
		FindObjectOfType<PlayerController>().GetComponent<HealthManager>().OnHit += UpdateHealthBar;
		healthBar = GameObject.Find("HealthbarG");
	}

	public void Update()
	{
		//FindObjectOfType<PlayerController>().GetComponent<HealthManager>().OnHealthChanged += UpdateHealthBar;
	}
	public void PlayGame()
	{
		Debug.Log("Game has Started");
		theEventManager.StartGame();
	}


	public void UpdateHealthBar(float obj)
	{
		float calc_Health = theHealthManager.currentHealth / theHealthManager.maxHealth; //if current health is 80 / 100 = 0.8f
		SetHealthBar(calc_Health);
	}

	public void SetHealthBar(float myHealth)
	{
		//myHealth needs to be a value between zero and one
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0, 1), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

	}


}

