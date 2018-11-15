using System.Collections;
using UnityEngine;

public class Points : MonoBehaviour
{
	//the points that a player recieves
	public int scoreToGive;
	//Reference to the score manager
	private ScoreManager theScoreManager;
	//Audio clip for when the enemy is killed
	public AudioClip enemyDeath;
	//The audio source
	private AudioSource audioSource;
	//This objects renderer
	public Renderer rend;



	// Use this for initialization
	void Start()
	{
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		audioSource = GetComponent<AudioSource>();
		theScoreManager = FindObjectOfType<ScoreManager>();
		//theHealthManager = GetComponent<HealthManager>();
	}

	// Update is called once per frame
	void Update()
	{
		//if (theHealthManager.currentHealth ==0)
		{
			theScoreManager.AddScore(scoreToGive);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			StartCoroutine(playAudio());
			//audioSource.Play();
			//gameObject.SetActive(false);
		}
	}

	public IEnumerator playAudio()
	{
		//Play Audio
		audioSource.Play();
		//Wait until it's done playing
		while (audioSource.isPlaying)
			yield return new WaitForSeconds(enemyDeath.length);

		//gameObject.SetActive(false);
		rend.enabled = true;
	}
}