using UnityEngine;
using System.Collections;

public class MonsterPatrol : MonoBehaviour {
	
	public float moveSpeed = 1f;
	public float sightDistance = .75f;
	public float storedTime;
	public bool frozen;

	// Update is called once per frame
	void Update () {

		if (!frozen) {
			storedTime += Time.deltaTime;

			//Move monster forward
			transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			Vector3 temp = transform.position;
			temp.y = Mathf.Sin (storedTime * 3f);
			transform.position = temp;


			//Raycast to prevent enemies from running into walls
			RaycastHit hit;
			Ray sightRay = new Ray (transform.position, transform.forward);
			Debug.DrawRay (transform.position, transform.forward * sightDistance);
			//Do a 180 when raycast hit
			if (Physics.Raycast (sightRay, out hit, sightDistance)) {
					transform.RotateAround (transform.position, transform.right, 180f);
			}
		}
	}
}
