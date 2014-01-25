using UnityEngine;
using System.Collections;

public class KeepMusicGoing : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}
