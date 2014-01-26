using UnityEngine;
using System.Collections;

public class MonsterAmbientSounds : MonoBehaviour {

	public float minSoundPeriod = 5.0f;
	public float maxSoundPeriod = 15.0f;

	private float timer;

	void Start() {
		timer = Random.Range (minSoundPeriod, maxSoundPeriod);
	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			audio.Play();
			timer = Random.Range (minSoundPeriod, maxSoundPeriod);
			print (name + " time till next sound: " + timer);
		}
	}
}
