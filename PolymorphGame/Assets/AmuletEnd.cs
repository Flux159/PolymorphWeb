using UnityEngine;
using System.Collections;

public class AmuletEnd : MonoBehaviour {
	public float endTime;
	bool beginEnd;
	float timer;
	GameObject amulet;
	Light amuletLight;
	public float rotateSpeed;
	bool rendererGone;
	Camera[] cameras;
	// Use this for initialization
	void Start () {
		endTime = 3f;
		beginEnd = false;
		timer = 0f;
		amulet = gameObject.transform.GetChild (0).gameObject;
		amuletLight = amulet.GetComponent<Light> ();
		rendererGone = false;
	}
	
	// Update is called once per frame
	void Update () {
		amulet.transform.Rotate (new Vector3 (rotateSpeed, rotateSpeed, rotateSpeed));
		if(beginEnd)
		{
			if(!rendererGone)
			{
				amuletLight.intensity += 0.7f;
				amuletLight.range += 5f;
			}
			timer+= Time.deltaTime;
			if(timer > endTime) Application.LoadLevel(0);
			if(!rendererGone && amuletLight.intensity > 7 && amuletLight.range > 40f)
			{
				RemoveRenderers();
				rendererGone = true;
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player") {
						beginEnd = true;
						collider.gameObject.GetComponentInChildren<OVRCameraController>().BackgroundColor = Color.white;
						cameras = collider.gameObject.GetComponentsInChildren<Camera>();
				}

	}

	public void RemoveRenderers()
	{
//		Debug.Log ("worked");
		foreach(Camera c in cameras)
		{
			c.backgroundColor = Color.white;
			c.cullingMask = (1 << 8);

		}
	}
}
