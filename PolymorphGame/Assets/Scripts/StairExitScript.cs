using UnityEngine;
using System.Collections;

public class StairExitScript : MonoBehaviour
{
		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.tag == "Player") {
						Application.LoadLevel (Application.loadedLevel + 1);
				}
		}
		/*
		void OnCollisionEnter (Collision collision)
		{
				if (collision.gameObject.tag == "Player") {
						Application.LoadLevel (Application.loadedLevel + 1);
				}
		}

		// Update is called once per frame
		void Update ()
		{
	
		}

*/
}
