using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToLoad : MonoBehaviour
{
	public void GotoMainScene()
	{
		SceneManager.LoadScene("UltraStellarTest");
	}

	public void GotoMenuScene()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}