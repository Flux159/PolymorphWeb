using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LightDimmer : MonoBehaviour {
	public float baseDirLightIntensity;
	public float baseTorchIntensity;
	public float torchIntensity;
	Light dirLight;
	Light[] torches;
	GameObject[] torchesObjs;
	public string tagName;
	public string dirLightName;
	public Color dimAmbientColor;
	public Color normAmbientColor;

	// Use this for initialization
	void Start () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag (tagName);
		foreach (GameObject obj in objs) 
		{
			if(obj.name == dirLightName) dirLight = obj.GetComponent<Light>();
			else if(obj.name == "Torches") 
			{
				torches = obj.GetComponentsInChildren<Light>();
				//torchesObjs = obj.get
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DimLights()
	{
		dirLight.intensity = 0f;
		foreach (Light t in torches)
		{
			//if(t.enabled) t.enabled = false;
			t.intensity = torchIntensity;
		}
		RenderSettings.ambientLight = dimAmbientColor;
				
	}

	public void UnDimLights()
	{
		dirLight.intensity = baseDirLightIntensity;
		foreach (Light t in torches)
		{
			t.intensity = baseTorchIntensity;
		}
		RenderSettings.ambientLight = normAmbientColor;
	}
}
