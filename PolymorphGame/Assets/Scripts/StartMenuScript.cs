using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class StartMenuScript : MonoBehaviour {

	public float xValLeft = 307.7f;
	public float xValRight = 846.4f;
	public float yVal = 241.5f;
	public float yValButton = 314.14f;
	
	public GUISkin skin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI() {

		GUI.skin = skin;

		GUI.Label (new Rect(xValLeft,yVal,200,200), "Polymorph");
		GUI.Label (new Rect(xValRight,yVal,200,200), "Polymorph");

		if (GUI.Button (new Rect (xValLeft, yValButton, 100, 100), "Start") ||
		    GUI.Button (new Rect (xValRight, yValButton, 100, 100), "Start")) {
				//Load scene
			Application.LoadLevel (Application.loadedLevel + 1);
		}

	}
}
