using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
	//Note this script can be used for any object that hurts the player

	public int damageToGive;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			//Deal damage to the player based on the defined value of damageToGive
			other.gameObject.GetComponent<HealthManager>().DamagePlayer(damageToGive);
		}
	}
}
