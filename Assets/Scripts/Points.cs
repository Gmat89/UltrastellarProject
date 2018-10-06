using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{

	public int scoreToGive;

	private ScoreManager theScoreManager;
	private EnemyHealthManager theEnemyHealthManager;
	public AudioClip enemyDeath;
	private AudioSource audioSource;
	public Renderer rend;



	// Use this for initialization
	void Start()
	{
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		audioSource = GetComponent<AudioSource>();
		theScoreManager = FindObjectOfType<ScoreManager>();
		theEnemyHealthManager = GetComponent<EnemyHealthManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (theEnemyHealthManager.currentHealth ==0)
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