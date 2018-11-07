using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

	public EventManager theEventManager;

	public void PlayGame()
	{
		Debug.Log("Game has Started");
		theEventManager.StartGame();
	}
}

