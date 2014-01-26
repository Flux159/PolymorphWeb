using UnityEngine;
using System.Collections;

public class FishCharacter : MonsterFocus {

	public AudioClip fishDeath;

	public override bool OnFocus() {
		base.OnFocus ();

		HideWaterTiles ();
		GameObject.Find ("Directional light").GetComponent<LightDimmer> ().UnDimLights ();
		DoFog ();

		return true;
	}
	void OnDestroy() {
		audio.clip = fishDeath;
		audio.Play ();
	}
}

