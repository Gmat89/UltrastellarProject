using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource Track1;

	public static SoundManager instance = null
		; //Static instance of GameManager which allows it to be accessed by any other script.

    private void Start()
    {
        Track1.Play();
    }
    //Awake is always called before any Start functions
    void Awake()
	{
	//Check if instance already exists
		if (instance == null)

	//if not, set instance to this
			instance = this;

	//If instance already exists and it's not this:
		else if (instance != this)

	//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

	//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}
  
}