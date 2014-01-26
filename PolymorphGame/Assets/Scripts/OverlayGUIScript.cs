using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class OverlayGUIScript : MonoBehaviour
{

		LTRect centerRectLeft;
		LTRect centerRectRight;

		LTRect imageRectLeft;
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

		private float mapYValue = 0.7f;

		void Start ()
		{
				centerRectLeft = CenterRect (200f, 200f, true);
				centerRectRight = CenterRect (200f, 200f, false);
				
				imageRectLeft = CenterRect (200f, 200f, true);
				imageRectRight = CenterRect (200f, 200f, false);

				i = 0;
				currentLabel = currentLabels [i];
				i++;

				centerRectLeft.alpha = 0.0f;
				centerRectRight.alpha = 0.0f;

				LeanTween.alpha (centerRectLeft, 1.0f, fadeTime);
				LeanTween.alpha (centerRectRight, 1.0f, fadeTime);

				StartCoroutine (PrintNextLabels (2.0f));
		}

		IEnumerator PrintNextLabels (float waitTime)
		{
				yield return new WaitForSeconds (waitTime);
				
				LeanTween.alpha (centerRectLeft, 0.0f, fadeTime);
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
		
				LeanTween.alpha (centerRectLeft, 1.0f, fadeTime);
				LeanTween.alpha (centerRectRight, 1.0f, fadeTime);

				StartCoroutine (PrintNextLabels (nextDialogTime));
		}

		LTRect CenterRect (float width, float height, bool left)
		{
				if (left) {
						float x = Screen.width / 4.0f - width / 2.0f;
						float y = Screen.height / 2.0f;
						return new LTRect (x, y, width, height);
				} else {
						float x = 3f * Screen.width / 4.0f - width / 2.0f;
						float y = Screen.height / 2.0f;
						return new LTRect (x, y, width, height);
				}
				
		}

		void OnGUI ()
		{

				if (displayLabels) {
						GUI.Label (centerRectLeft.rect, currentLabel);
						GUI.Label (centerRectRight.rect, currentLabel);
				}

				//'E' for keyboard, 'B' for controller
//				if (CameraController.transform.GetChild (0).rotation.eulerAngles.y > mapYRotation) {
				if (CameraController.transform.GetChild (0).forward.normalized.y > mapYValue) {
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
