using UnityEngine;
using System.Collections;

public class RatCharacter : MonsterFocus {

	public AudioClip ratDeath;

	public override bool OnFocus() {
		base.OnFocus ();

		ShowWaterTiles ();
		GameObject.Find ("Directional light").GetComponent<LightDimmer> ().UnDimLights ();
		UnDoFog ();

		return true;
	}

	void OnDestroy() {
		audio.clip = ratDeath;
		audio.Play ();
	}
}