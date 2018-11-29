using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{

	public GameManager theGameManager;
	public ScoreManager theScoreManager;
	public WaveSpawner theWaveSpawner;


	void Start()
	{
		theScoreManager = FindObjectOfType<ScoreManager>();
		theGameManager = FindObjectOfType<GameManager>();
		theWaveSpawner = FindObjectOfType<WaveSpawner>();
	}
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	    theScoreManager.gameObject.SetActive(true);
	    theScoreManager.gameObject.SetActive(true);
	    theWaveSpawner.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("BUTTONPRESSED");
    }

}
