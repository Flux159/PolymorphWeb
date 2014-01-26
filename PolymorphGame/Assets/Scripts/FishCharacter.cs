using UnityEngine;
using System.Collections;

public class FishCharacter : MonsterFocus {

	public AudioClip fishDeath;

	public override bool OnFocus() {
		GameObject newCameraController = (GameObject) Instantiate (newPlayerController, transform.position, transform.rotation);
		OverlayGUIScript.CameraController = newCameraController.GetComponentInChildren<OVRCameraController>().gameObject;

		transform.gameObject.SetActive (false);

		HideWaterTiles ();
		if (dimmer != null) { dimmer.UnDimLights (); }
		DoFog ();

		return true;
	}
	void OnDestroy() {
		audio.clip = fishDeath;
		audio.Play ();
	}
}

