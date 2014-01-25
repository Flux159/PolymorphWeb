using UnityEngine;
using System.Collections;

public class LightTimer : MonoBehaviour {
	private float fTimer = 0;
	private float life = 0.5f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		fTimer += Time.deltaTime;
		if (fTimer < life)
						Destroy (gameObject);
	}
}
