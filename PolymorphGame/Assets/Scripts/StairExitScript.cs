using UnityEngine;
using System.Collections;

public class StairExitScript : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
		
		}

		void OnCollisionEnter (Collision collision)
		{
				if (collision.transform.tag == "Player") {
						Application.LoadLevel (Application.loadedLevel + 1);
				}
		}

		// Update is called once per frame
		void Update ()
		{
	
		}
}
