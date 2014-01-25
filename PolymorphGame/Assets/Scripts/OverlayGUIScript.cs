using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class OverlayGUIScript : MonoBehaviour
{

		LTRect centerRectLeft;
		LTRect centerRectRight;

		bool displayLabels = true;

		string[] currentLabels = {"This is the story of a cursed soul", "A human soul forced to become the monster he sees...",
	"When just a child this curse was placed upon the sorry human soul and destroyed everything it held dear."};
		string currentLabel;
		int i;

		public float fadeTime = 1.0f;
		public float nextDialogTime = 2.0f;
//		private float counter = 0.0f;

		void Start ()
		{
				centerRectLeft = CenterRect (200f, 200f, true);
				centerRectRight = CenterRect (200f, 200f, false);
				
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
