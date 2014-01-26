using UnityEngine;
using System.Collections;

public class RatCharacter : MonsterFocus {

	public AudioClip ratDeath;

	public override bool OnFocus() {
		// Get all the waters and disable them!!
		GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
		foreach (GameObject w in waters) {
			w.SetActive (true);
		}
		RenderSettings.fog = false;
		RenderSettings.fogDensity = 0.0f;

		return true;
	}
	void OnDestroy() {
		audio.clip = ratDeath;
		audio.Play ();
	}
}