using UnityEngine;
using System.Collections;

public class FishFocus : MonsterFocus {

	public override bool OnFocus() {
		// Get all the waters and disable them!!
		GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
		foreach (GameObject w in waters) {
			w.SetActive (false);
		}

		//TODO: CHRIS'S AWESOME FISH STUFF
	}
