using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToLoad : MonoBehaviour
{
	
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