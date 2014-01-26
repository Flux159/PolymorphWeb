using UnityEngine;
using System.Collections;

public class FishCharacter : MonsterFocus {

	public float fogDensity = 0.5f;

	public override bool OnFocus() {
		// Get all the waters and disable them!!
		GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
		foreach (GameObject w in waters) {
			w.GetComponentInChildren<MeshRenderer>().enabled = false;
			//w.SetActive (false);
		}

		//CHRIS'S AWESOME FISH STUFF

		RenderSettings.fog = true;
		RenderSettings.fogDensity = fogDensity;
		RenderSettings.fogColor = new Color (0, 0.1f, 0.12f, .1f);

		Instantiate (newPlayerController, transform.gameObject.transform.position, transform.gameObject.transform.rotation);
		transform.gameObject.SetActive (false);

		return true;
	}

}

