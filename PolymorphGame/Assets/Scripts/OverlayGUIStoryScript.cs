using UnityEngine;
using System.Collections;

public class OverlayGUIStoryScript : MonoBehaviour
{

//		LTRect centerRectLeft;
		LTRect centerRectRight;
	
//		LTRect imageRectLeft;
		LTRect imageRectRight;
	
		bool displayLabels = true;
	
		public Texture MinimapTexture;
		public GameObject CameraController;
	
		string[] currentLabels = {"This is the story of a cursed soul", "A human soul forced to become the monster he sees...",
		"The human was banished from its homeland and forced to live alone.", 
		"Then a rumor came to the poor souls ear...", "A rumor of an amulet that could grant any wish, held deep in a cave.",
		"Thus began our cursed soul's journey..."};
		string currentLabel;
		int i;
	
		public float fadeTime = 1.0f;
		public float nextDialogTime = 2.0f;
		//		private float counter = 0.0f;
	
		private float mapYValue = 0.6f;

		public float xValLeft = 275f;
		public float xValRight = 875f;

		public GUISkin skin;

		void Start ()
		{
//				centerRectLeft = CenterRect (200f, 200f, true);
				centerRectRight = CenterRect (200f, 200f, false);
		
//				imageRectLeft = ImageCenterRect (200f, 200f, true);
				imageRectRight = ImageCenterRect (200f, 200f, false);
		
				i = 0;
				currentLabel = currentLabels [i];
				i++;
		
//				centerRectLeft.alpha = 0.0f;
				centerRectRight.alpha = 0.0f;
		
//				LeanTween.alpha (centerRectLeft, 1.0f, fadeTime);
				LeanTween.alpha (centerRectRight, 1.0f, fadeTime);
		
				StartCoroutine (PrintNextLabels (2.0f));
		}
	
		IEnumerator PrintNextLabels (float waitTime)
		{
				yield return new WaitForSeconds (waitTime);
		
//				LeanTween.alpha (centerRectLeft, 0.0f, fadeTime);
				LeanTween.alpha (centerRectRight, 0.0f, fadeTime);
		
				StartCoroutine (PrintSecondLabels (nextDialogTime));
		}
	
		IEnumerator PrintSecondLabels (float waitTime)
		{
				yield return new WaitForSeconds (waitTime);
		
				if (i < currentLabels.Length) {
						currentLabel = currentLabels [i];
						i++;
				} else {
						currentLabel = "";
						displayLabels = false;
						StopAllCoroutines ();
						return false;
				}
		
//				LeanTween.alpha (centerRectLeft, 1.0f, fadeTime);
				LeanTween.alpha (centerRectRight, 1.0f, fadeTime);
		
				StartCoroutine (PrintNextLabels (nextDialogTime));
		}
	
		LTRect CenterRect (float width, float height, bool left)
		{
				if (left) {
						//						float x = Screen.width / 4.0f - width / 1.5f;
						//						float y = Screen.height / 2.0f;
//						float x = xValLeft;
						float x = Screen.width / 2.0f - width / 2.0f;
						float y = 400f;
						return new LTRect (x, y, width, height);
				} else {
						//						float x = 3f * Screen.width / 4.0f - width / 1.5f;
						//						float y = Screen.height / 2.0f;
//						float x = xValRight;
						float x = Screen.width / 2.0f - width / 2.0f;
						float y = 400f;
						return new LTRect (x, y, width, height);
				}
		
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

				GUI.skin = skin;
		
				if (displayLabels) {
//						OVRGUI ovrHelper = new OVRGUI ();
//						OVRCameraController controller = CameraController.GetComponent (typeof(OVRCameraController)) as OVRCameraController;
//						ovrHelper.SetCameraController (ref controller);
//
//						int w = Screen.width;
//						int h = Screen.height;
//
//						ovrHelper.SetDisplayResolution (OVRDevice.HResolution, OVRDevice.VResolution);
//						ovrHelper.SetPixelResolution (w, h);
//
//						string label = "Testing";
//
//						ovrHelper.StereoBox (centerRectLeft.rect.x, centerRectLeft.rect.y, centerRectLeft.rect.width, centerRectLeft.rect.height, ref currentLabel, Color.white);

//						GUI.Label (CenterRect (200f, 200f, true).rect, "Testing");
//						GUI.Label (CenterRect (200f, 200f, false).rect, "Testing");

						//Don't display this
//						GUI.Label (centerRectLeft.rect, currentLabel);
						GUI.Label (centerRectRight.rect, currentLabel);
				}
		
				//'E' for keyboard, 'B' for controller
				//				if (CameraController.transform.GetChild (0).rotation.eulerAngles.y > mapYRotation) {
				if (CameraController.transform.GetChild (0).forward.normalized.y > mapYValue) {
//						GUI.DrawTexture (imageRectLeft.rect, MinimapTexture);
						GUI.DrawTexture (imageRectRight.rect, MinimapTexture);
				}
		
				//				counter += Time.deltaTime;
				//
				//				if (nextDialog < counter) {
				//						LeanTween.alpha (centerRectLeft, 0.0f, 1.0f);
				//						LeanTween.alpha (centerRectRight, 0.0f, 1.0f);
				//				}
		}
}
