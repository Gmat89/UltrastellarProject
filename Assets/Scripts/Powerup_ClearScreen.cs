using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_ClearScreen : MonoBehaviour
{

    public GameObject pickupEffect;
    public ParticleSystem pickupParticle;
    private float powerupDuration = 4.0f;

    private GameObject[] gameObjects;


    // Use this for initialization
    void Start()
    {
        pickupParticle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void DestoryAllEnemies()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < gameObjects.Length; i++)
        {

            Destroy(gameObjects[i]);

        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            pickupParticle.Clear();
            //DestoryAllEnemies();
            StartCoroutine(Pickup());
        }
    }

    IEnumerator Pickup()
    {
        //Spawn Effect when pick is collected
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //Apply the effect to the Player
        DestoryAllEnemies();

        //Disable the mesh renderer when pickup is obtained
        GetComponent<MeshRenderer>().enabled = false;
        //Disable collider when the pickup is obtained.
        GetComponent<Collider>().enabled = false;
        //Destroy the particle
        Destroy(pickupParticle);

        //Wait x amount of seconds
        yield return new WaitForSeconds(powerupDuration);

        //Destory the Pickup object
        Destroy(gameObject);
    }
}
