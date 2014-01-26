using UnityEngine;
using System.Collections;

public class BatCharacter : MonsterFocus {

	public AudioClip batDeath;

	public override bool OnFocus() {
		// Get all the waters and disable them!!
		GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
		foreach (GameObject w in waters) {
			w.SetActive (false);
		}
		
		//CHRIS'S AWESOME FISH STUFF
		
		RenderSettings.fog = false;
		RenderSettings.fogDensity = 0.0f;
		
		return true;
	}
	void OnDestroy() {
		audio.clip = batDeath;
		audio.Play ();
		}
}

