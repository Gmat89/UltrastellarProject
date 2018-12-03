using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_PlayerSpeedUp : MonoBehaviour
{
    public PlayerController player;
    public GameObject playerPrefab;
    public float ROFPower;
    public GameObject pickupEffect;
    public ParticleSystem pickupParticle;
    public float powerupDuration = 4.0f;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        pickupParticle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickupParticle.Clear();
            StartCoroutine(Pickup());
        }
    }

    IEnumerator Pickup()
    {
        //Spawn Effect when pick is collected
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //Apply the effect to the Player
        player.moveSpeed += ROFPower;

        if (player.moveSpeed < 5)
        {
            player.moveSpeed = Mathf.Clamp(player.moveSpeed, 0, player.moveSpeed);
        }
        if (player.moveSpeed > 10)
        {
            player.moveSpeed = Mathf.Clamp(player.moveSpeed, 0, player.moveSpeed);
        }

        //Disable the mesh renderer when pickup is obtained
        GetComponent<SpriteRenderer>().enabled = false;
        //Disable collider when the pickup is obtained.
        GetComponent<Collider>().enabled = false;
        //Destroy the particle
        Destroy(pickupParticle);

        //Wait x amount of seconds
        yield return new WaitForSeconds(powerupDuration);
        //Reverse the effect on the player (reset back to default firing rate)
        player.moveSpeed -= ROFPower;

        //Destory the Pickup object
        Destroy(gameObject);
    }

}
