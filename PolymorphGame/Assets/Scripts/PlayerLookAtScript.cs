using UnityEngine;
using System.Collections;

public class PlayerLookAtScript : MonoBehaviour
{

	public GameObject particlePrefab;
	public float timeToActivate = 4.0f;
	private float timer;
	

	// Use this for initialization
	void Start ()
	{
	}



	// Update is called once per frame
	void Update ()
	{
		GameObject leftCamera = GameObject.Find ("CameraLeft");
		Ray leftRay = new Ray (leftCamera.transform.position, leftCamera.transform.forward);
		RaycastHit leftHit = new RaycastHit ();

		GameObject rightCamera = GameObject.Find ("CameraRight");
		Ray rightRay = new Ray (rightCamera.transform.position, rightCamera.transform.forward);
		RaycastHit rightHit = new RaycastHit ();

	
		if (Physics.Raycast (leftRay, out leftHit, 100) && Physics.Raycast (rightRay, out rightHit, 10) &&
			leftHit.collider == rightHit.collider) {

			bool hitSelf = (leftHit.collider.gameObject.name == "OVRPlayerController");
			if (hitSelf) {
				print ("GOD DAMN IT");
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
			}

			if (focus != null && focus.IsFocusable ()) {
				timer += Time.deltaTime;
				Instantiate (particlePrefab, leftHit.point, Quaternion.identity);
				if (timer > timeToActivate) {
					bool timeToDie = focus.OnFocus ();
					if (timeToDie) {
						transform.parent.gameObject.SetActive(false);
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
		Gizmos.color = Color.yellow;
		float lineLength = 5.0f;

		GameObject leftCamera = GameObject.Find ("CameraLeft");
		Vector3 leftTarget = leftCamera.transform.position + leftCamera.transform.forward * lineLength;
		Gizmos.DrawLine (leftCamera.transform.position, leftTarget);

		GameObject rightCamera = GameObject.Find ("CameraRight");
		Vector3 rightTarget = rightCamera.transform.position + rightCamera.transform.forward * lineLength;
		Gizmos.DrawLine (rightCamera.transform.position, rightTarget);
	}
}
