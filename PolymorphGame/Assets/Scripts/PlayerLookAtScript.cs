using UnityEngine;
using System.Collections;

public class PlayerLookAtScript : MonoBehaviour
{



	class ChangedObject
	{
		public Renderer renderer;
		public Material originalMaterial;
		
		public ChangedObject (Renderer renderer, Material material)
		{
			this.renderer = renderer;
			originalMaterial = renderer.sharedMaterial;
			renderer.material = material;
		}
	}

	ChangedObject changedObject;
	Material redMaterial;
	

	// Use this for initialization
	void Start ()
	{
		//rightCamera = GameObject.Find("CameraRight");

		redMaterial = new Material (Shader.Find ("Diffuse"));
		redMaterial.color = Color.red;
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

	
		if (Physics.Raycast (leftRay, out leftHit, 100) && Physics.Raycast (rightRay, out rightHit, 100) &&
			leftHit.collider == rightHit.collider) {

			bool hitSelf = (leftHit.collider.gameObject.name == "OVRPlayerController");
			if (hitSelf) {
				print ("GOD DAMN IT");
				return;
			}


			MeshRenderer hitRenderer = leftHit.collider.gameObject.GetComponent<MeshRenderer> ();
			//if (null == hitRenderer) {print ("no renderer on object: " + leftHit.collider.gameObject.name);}
		
			if (changedObject != null) {
				if (changedObject.renderer == hitRenderer) {
					return;
				} else {
					changedObject.renderer.material = changedObject.originalMaterial;
				}
			}
		
			changedObject = new ChangedObject (hitRenderer, redMaterial);

		} else if (changedObject != null) {
			changedObject.renderer.material = changedObject.originalMaterial;
			changedObject = null;
		}
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
