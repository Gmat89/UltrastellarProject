using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class UIManager : MonoBehaviour
{

	//public EnemyController theEnemyController;
	public GameObject thePlayerHealthBar;
	public HealthManager theHealthManager;
	//public GameObject theEnemyHealthBar;

	//EnemyController findObjofType;



	public void Awake()
	{
		//On awake find the game manager and subscribe to the on player spawned event then listen for this scripts onplayerspawned event
		FindObjectOfType<GameManager>().OnPlayerSpawned += OnPlayerSpawned;
			
		// TODO: Sub to player death and unsub health
	}

	private void OnPlayerSpawned(PlayerController obj)
	{
		//Find the health manager attached to the object with the player controller
		theHealthManager = obj.GetComponent<HealthManager>();
		//Subscribe to the Player controller/ health managers event OnHealthChanged and run the UpdateHealthbar function accordingly
		obj.theHealthManager.OnHealthChanged += UpdateHealthBar;
	}

	public void Update()
	{

	}


	public void UpdateHealthBar(float calc_Health)
	{
		calc_Health = theHealthManager.currentHealth / theHealthManager.maxHealth; //if current health is 80 / 100 = 0.8f
		//Run the SetHealthBar function
		SetHealthBar(calc_Health);
	}

	public void SetHealthBar(float myHealth)
	{
		//myHealth needs to be a value between zero and one
		thePlayerHealthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0, 1), thePlayerHealthBar.transform.localScale.y, thePlayerHealthBar.transform.localScale.z);
		
		//Doesnt work
		//theEnemyHealthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0, 1), theEnemyHealthBar.transform.localScale.y, theEnemyHealthBar.transform.localScale.z);
	}
}

