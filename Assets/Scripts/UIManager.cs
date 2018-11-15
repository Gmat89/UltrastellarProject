using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

	public EnemyController theEnemyController;
	public GameObject thePlayerHealthBar;
	public HealthManager theHealthManager;
	public GameObject theEnemyHealthBar;

	PlayerController findObjectOfType;
	//EnemyController findObjofType;



	public void Start()
	{

		findObjectOfType = FindObjectOfType<PlayerController>();
		theHealthManager = findObjectOfType.GetComponent<HealthManager>();
		theHealthManager.OnHealthChanged += UpdateHealthBar;

		//findObjofType = FindObjectOfType<EnemyController>();
		//theHealthManager = findObjofType.GetComponent<HealthManager>(); Ask cam about fixing this 
		theHealthManager.OnHealthChanged += UpdateHealthBar;
		
		theEnemyHealthBar = GameObject.Find("Bar");
	}

	public void Update()
	{
		
	}
	/*public void PlayGame()
	{
		Debug.Log("Game has Started");
		theEventManager.StartGame();
	}*/


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
		//theEnemyHealthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0, 1), theEnemyHealthBar.transform.localScale.y, theEnemyHealthBar.transform.localScale.z);
	}


}

