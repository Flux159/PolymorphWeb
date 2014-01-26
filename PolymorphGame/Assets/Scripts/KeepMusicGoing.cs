using UnityEngine;
using System.Collections;

public class KeepMusicGoing : MonoBehaviour {
	public static GameObject instance = null;
	public static GameObject Instance {
		get { return instance; }
	}
	public AudioClip footsteps;

	void Awake() {
		//If one already exists, play footsteps (new level).
		//Otherwise, mark this as the permanent music player.
		print ("INSTANCE = " + instance);
		if (instance != null && instance != this.gameObject) {
			audio.PlayOneShot(footsteps);
			GameObject.Destroy(this,10f);
		} else {
			print ("SAVING INSTANCE, MARKING AWESOME");
			instance = this.gameObject;
			DontDestroyOnLoad (this.gameObject);
		}
	}
}
