using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

	GameManager GM;

	void Awake()
	{
		GM = GameManager.instance;
		Debug.Log("Current game state when Awakes: " + GM.gameState);
	}

	void Start()
	{
		GM.SetGameState(GameState.MAIN_MENU);
		Debug.Log("Current game state when Starts: " + GM.gameState);

	}

	public void HandleOnStateChange()
	{
		
		Debug.Log("Handling state change to: " + GM.gameState);
		Invoke("LoadLevel", 3f);
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene("MainMenu");
	}


}