using UnityEngine;
using System.Collections;

public class LoadLevelColliderScript : MonoBehaviour
{
		public string levelName;

		void OnCollisionEnter (Collision collision)
		{
				print ("OnCollisionEnter: " + collision.gameObject.tag);
				if (collision.gameObject.tag == "Player") {
						Application.LoadLevel (levelName);
				}
		
		}

		void OnTriggerEnter (Collider other)
		{
				print ("OnTriggerEnter: " + other.gameObject.tag);
				if (other.gameObject.tag == "Player") {
						Application.LoadLevel (levelName);
				}

		}
}
