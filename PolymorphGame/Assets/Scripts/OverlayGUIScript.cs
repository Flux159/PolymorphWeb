using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class OverlayGUIScript : MonoBehaviour
{
	
		LTRect imageRectLeft;
		LTRect imageRectRight;
	
		public Texture MinimapTexture;
		public GameObject StartingCameraController;
		public static GameObject CameraController;

		private float mapYValue = 0.6f;

		void Start ()
		{
			CameraController = StartingCameraController;
			imageRectLeft = ImageCenterRect (200f, 200f, true);
			imageRectRight = ImageCenterRect (200f, 200f, false);
		}

		LTRect ImageCenterRect (float width, float height, bool left)
		{
				if (left) {
						//						float x = Screen.width / 4.0f - width / 1.5f;
						//						float y = Screen.height / 2.0f;
						float x = 330f;
						float y = 200f;
						return new LTRect (x, y, width, height);
				} else {
						//						float x = 3f * Screen.width / 4.0f - width / 1.5f;
						//						float y = Screen.height / 2.0f;
						float x = 930f;
						float y = 200f;
						return new LTRect (x, y, width, height);
				}
		}
	
		void OnGUI ()
		{
				//First... need to know if you're a FISH or ANYTHING ELSE.
				// Hacky but game jam!
				OVRPlayerController p = CameraController.transform.parent.GetComponent<OVRPlayerController> ();
				bool isFish = p.fish;

				//'E' for keyboard, 'B' for controller
				bool showMap = (isFish) ?
					(CameraController.transform.GetChild (0).forward.normalized.y < -mapYValue)
				  : (CameraController.transform.GetChild (0).forward.normalized.y > mapYValue);
					
				if (showMap) {
					GUI.DrawTexture (imageRectLeft.rect, MinimapTexture);
					GUI.DrawTexture (imageRectRight.rect, MinimapTexture);
				}

//				counter += Time.deltaTime;
//
//				if (nextDialog < counter) {
//						LeanTween.alpha (centerRectLeft, 0.0f, 1.0f);
//						LeanTween.alpha (centerRectRight, 0.0f, 1.0f);
//				}
		}
	
		// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
