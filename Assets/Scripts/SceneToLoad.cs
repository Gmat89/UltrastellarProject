using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToLoad : MonoBehaviour
{
	//GameManager Reference
	private GameManager GM;

	//As soon as the game is played
	void Awake()
	{
		//Store an instance of the GameManager
		GM = GameManager.instance;
		//Subcscribe to a change in game state through the GM event
		GM.OnStateChange += HandleOnStateChange;
	}
	//Respond to the state change event
	public void HandleOnStateChange()
	{
		Debug.Log("OnStateChange!");
	}
	

	public void StartGame()
	{
		//start game scene
		GM.SetGameState(GameState.GAME);
		Debug.Log(GM.gameState);
	}


	//Main Game Function
	public void GotoMainScene()
	{
		//Load the Main Game Scene
		SceneManager.LoadScene("UltraStellarTest");
	}
	//Main Menu Function
	public void GotoMenuScene()
	{
		//Load the Main Menu Scene
		SceneManager.LoadScene("MainMenu");
	}
	//Quit Function
	public void QuitGame()
	{
		//Quit the game
		Application.Quit();
	}

}