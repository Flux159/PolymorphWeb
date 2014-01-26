using UnityEngine;
using System.Collections;

public class RatCharacter : MonsterFocus {

	public AudioClip ratDeath;

	public override bool OnFocus() {
		//base.OnFocus (); // SKIP THE PARENT
		
		GameObject newCameraController = (GameObject) Instantiate (newPlayerController, transform.position+Vector3.up*1.0f, transform.rotation);
		OverlayGUIScript.CameraController = newCameraController.GetComponentInChildren<OVRCameraController>().gameObject;

		transform.gameObject.SetActive (false);

		ShowWaterTiles ();
		if (dimmer != null) { UnDimLights (); }
		UnDoFog ();

		return true;
	}

	void OnDestroy() {
		audio.clip = ratDeath;
		audio.Play ();
	}
}