using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
	public delegate void StartGameDelegate();

	public  StartGameDelegate onStartGame;

	public delegate void Action();

	public event Action OnPlayerDeath;
	//public event Action OnChangeColour;
	//public event Action OnPlayerHit;


	public void StartGame()
	{
		if (onStartGame != null)
		{
			onStartGame();
		}
	}

}