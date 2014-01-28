using UnityEngine;
using System.Collections;

public class MonsterFocus : MonoBehaviour, IFocusable
{
		public bool alive = true;
		public GameObject newPlayerController;
		protected LightDimmer dimmer;

		void Awake ()
		{
				dimmer = GameObject.Find ("Directional light").GetComponent<LightDimmer> ();
		}

		public virtual bool OnFocus ()
		{
				print ("PARENT MONSTER FOCUS GOOOOO");
				GameObject newCameraController = (GameObject)Instantiate (newPlayerController, transform.position, transform.rotation);
				OverlayGUIScript.CameraController = newCameraController.GetComponentInChildren<NonOVRCameraController> ().gameObject;

				transform.gameObject.SetActive (false);

				//GameObject.Destroy (transform.gameObject, 2.0f);
				return true;
		}

		public virtual bool IsFocusable ()
		{
				return alive;
		}
	
		public void DoFog ()
		{
				float fogDensity = 0.5f;
				RenderSettings.fog = true;
				RenderSettings.fogDensity = fogDensity;
				RenderSettings.fogColor = new Color (0, 0.1f, 0.12f, .1f);
		}
		public void UnDoFog ()
		{
				RenderSettings.fog = false;
				RenderSettings.fogDensity = 0.0f;
		}


		public void ShowWaterTiles ()
		{
				GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
				foreach (GameObject w in waters) {
						w.GetComponent<MeshRenderer> ().enabled = true;
				}
		}
		public void HideWaterTiles ()
		{
				GameObject[] waters = GameObject.FindGameObjectsWithTag ("Water");
				foreach (GameObject w in waters) {
						w.GetComponent<MeshRenderer> ().enabled = false;
				}
		}

		public void DimLights ()
		{
				if (dimmer != null) {
						dimmer.DimLights ();
				} else {
						Debug.LogError ("DIMMER NOT FOUND!!!");
				}
		}

		public void UnDimLights ()
		{
				if (dimmer != null) {
						dimmer.UnDimLights ();
				} else {
						Debug.LogError ("DIMMER NOT FOUND!!!");
				}
		}
}
