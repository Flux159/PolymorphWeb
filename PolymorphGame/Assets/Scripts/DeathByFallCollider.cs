using UnityEngine;
using System.Collections;

public class DeathByFallCollider : MonoBehaviour
{
	
	/*
	void OnCollisionEnter (Collision collision)
	{
		print ("OnCollisionEnter: " + collision.gameObject.tag);
		if (collision.gameObject.tag == "Player") {
			Application.LoadLevel (Application.loadedLevel);
		}
		
	}
*/

	void OnTriggerEnter (Collider other)
	{
		print ("DEATH BY FALL OnTriggerEnter!!! " + other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
