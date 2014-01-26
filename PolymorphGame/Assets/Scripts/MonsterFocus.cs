using UnityEngine;
using System.Collections;

public class MonsterFocus : MonoBehaviour, IFocusable
{
	public bool alive = true;
	public GameObject newPlayerController;
	
	public virtual bool OnFocus ()
	{
		Instantiate (newPlayerController, transform.gameObject.transform.position, transform.gameObject.transform.rotation);
			transform.gameObject.SetActive (false);
		transform.gameObject.SetActive (false);
		//GameObject.Destroy (transform.gameObject, 2.0f);
		return true;
	}

	public virtual bool IsFocusable ()
	{
		return alive;
	}
	
	public void DoFog() {
		float fogDensity = 0.5f;
		RenderSettings.fog = true;
		RenderSettings.fogDensity = fogDensity;
		RenderSettings.fogColor = new Color (0, 0.1f, 0.12f, .1f);
	}
	
	public void UnDoFog() {
		RenderSettings.fog = false;
		RenderSettings.fogDensity = 0.0f;
	}

	public void ShowWaterTiles() {
		GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
		foreach (GameObject w in waters) {
			w.GetComponentInChildren<MeshRenderer>().enabled = true;
		}
	}

	public void HideWaterTiles() {
		GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
		foreach (GameObject w in waters) {
			w.GetComponentInChildren<MeshRenderer>().enabled = false;
		}
	}
}
