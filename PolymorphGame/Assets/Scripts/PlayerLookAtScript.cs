using UnityEngine;
using System.Collections;

public class PlayerLookAtScript : MonoBehaviour
{
		public GameObject particlePrefab;
		public float timeToActivate = 4.0f;
		private float timer;
	
		GameObject leftCamera;

		GameObject rightCamera;

		private GameObject lifeBarObject;

		void Start ()
		{
				leftCamera = GameObject.Find ("CameraLeft");
				rightCamera = GameObject.Find ("CameraRight");
		}



		// Update is called once per frame
		void Update ()
		{
				Ray leftRay = new Ray (leftCamera.transform.position, leftCamera.transform.forward);
				RaycastHit leftHit = new RaycastHit ();

				Ray rightRay = new Ray (rightCamera.transform.position, rightCamera.transform.forward);
				RaycastHit rightHit = new RaycastHit ();

	
				if (Physics.Raycast (leftRay, out leftHit, 10) && Physics.Raycast (rightRay, out rightHit, 10) &&
						leftHit.collider == rightHit.collider) {

						bool hitSelf = (leftHit.collider.gameObject.name == "OVRPlayerController");
						if (hitSelf) {
//								print ("GOD DAMN IT");
								return;
						}

						GameObject hitObject = leftHit.collider.gameObject;

						// MeshRenderer hitRenderer = hitObject.GetComponent<MeshRenderer> ();
						//if (null == hitRenderer) {print ("no renderer on object: " + leftHit.collider.gameObject.name);}
						IFocusable focus = null;
						Component[] hitFocusables = hitObject.GetComponents (typeof(IFocusable));
						if (hitFocusables.Length > 0) {
								focus = (IFocusable)hitFocusables [0];
						}
						// changedObject = new ChangedObject (hitRenderer, redMaterial);
						// IFocusable focus = hitObject.GetComponent<DoorUp> () as IFocusable;

						if (focus == null) {
								//focus = leftHit.collider.gameObject.GetComponent<MonsterFocus> () as IFocusable;
								GameObject.Destroy (lifeBarObject);
								timer = 0;
						}

						if (focus != null && focus.IsFocusable ()) {
								timer += Time.deltaTime;
								if (lifeBarObject == null) {
										Vector3 lifeBarObjectForwardDirection = leftHit.transform.position - leftCamera.transform.position;
										Quaternion direction = Quaternion.identity;
										direction.SetLookRotation (lifeBarObjectForwardDirection, Vector3.up);
										lifeBarObject = (GameObject)Instantiate (particlePrefab, leftHit.point, direction);
								}
								if (timer > timeToActivate) {
										GameObject.Destroy (lifeBarObject);
										bool timeToDie = focus.OnFocus ();
										if (timeToDie) {
												transform.parent.gameObject.SetActive (false);
												//Destroy (transform.parent.gameObject);
										}
										timer = 0;
								}
						}
				}
		}


		float leftCrosshairX = Screen.width / 3.5f;
		float leftCrosshairY = Screen.height / 2.0f; // 2.03f;
	
		float rightCrosshairX = Screen.width - Screen.width / 3.5f; // 3.4f
		float rightCrosshairY = Screen.height / 2.0f; // 2.03f
	
		void OnGUI ()
		{
				// print ("Screen.width: " + Screen.width + " Screen.height: " + Screen.height);
				// print ("leftCrosshairX: " + leftCrosshairX + " leftCrosshairY: " + leftCrosshairY);
				GUI.Box (new Rect (leftCrosshairX, leftCrosshairY, 10, 10), "");
				// print ("rightCrosshairX: " + rightCrosshairX + " rightCrosshairY: " + rightCrosshairY);
				GUI.Box (new Rect (rightCrosshairX, rightCrosshairY, 10, 10), "");
		}
	
	
		void OnDrawGizmos ()
		{
				if (null != leftCamera && null != rightCamera) {	
						Gizmos.color = Color.yellow;
						float lineLength = 5.0f;
				
						Vector3 leftTarget = leftCamera.transform.position + leftCamera.transform.forward * lineLength;
						Gizmos.DrawLine (leftCamera.transform.position, leftTarget);

						Vector3 rightTarget = rightCamera.transform.position + rightCamera.transform.forward * lineLength;
						Gizmos.DrawLine (rightCamera.transform.position, rightTarget);
				}
		}
}
