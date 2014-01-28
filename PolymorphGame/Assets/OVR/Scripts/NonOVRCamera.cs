using UnityEngine;
using System.Collections;

public class NonOVRCamera : OVRComponent
{

		private NonOVRCameraController CameraController = null;

		// PUBLIC MEMBERS
		// camera position...	
		// From root of camera to neck (translation only)
		[HideInInspector]
		public Vector3
				NeckPosition = new Vector3 (0.0f, 0.0f, 0.0f);	
		// From neck to eye (rotation and translation; x will be different for each eye)
		[HideInInspector]
		public Vector3
				EyePosition = new Vector3 (0.0f, 0.09f, 0.16f);
	
		// STATIC MEMBERS
		// We will grab the actual orientation that is used by the cameras in a shared location.
		// This will allow multiple OVRCameraControllers to eventually be uused in a scene, and 
		// only one orientation will be used to syncronize all camera orientation
		static private Quaternion CameraOrientation = Quaternion.identity;

		// Use this for initialization
		void Start ()
		{
				base.Start ();

				CameraController = gameObject.transform.parent.GetComponent<NonOVRCameraController> ();
		
				if (CameraController == null)
						Debug.LogWarning ("WARNING: NonOVRCameraController not found!");
		}
	
		// Update is called once per frame
		void Update ()
		{
				base.Update ();
		}

		// OnPreCull
		void OnPreCull ()
		{
				// NOTE: Setting the camera here increases latency, but ensures
				// that all Unity sub-systems that rely on camera location before
				// being set to render are satisfied. 
				if (CameraController.CallInPreRender == false)
						SetCameraOrientation ();
		
		}
	
		// OnPreRender
		void OnPreRender ()
		{
				// NOTE: Better latency performance here, but messes up water rendering and other
				// systems that rely on the camera to be set before PreCull takes place.
				if (CameraController.CallInPreRender == true)
						SetCameraOrientation ();
		
				if (CameraController.WireMode == true)
						GL.wireframe = true;
		
//		// Set new buffers and clear color and depth
//		if (CameraTexture != null) {
//			Graphics.SetRenderTarget (CameraTexture);
//			GL.Clear (true, true, camera.backgroundColor);
//		}
		}

	#region OVRCamera Functions
		// SetCameraOrientation
		void SetCameraOrientation ()
		{
				Quaternion q = Quaternion.identity;
				Vector3 dir = Vector3.forward;		
		
				// Main camera has a depth of 0, so it will be rendered first
				if (camera.depth == 0.0f) {			
						// If desired, update parent transform y rotation here
						// This is useful if we want to track the current location of
						// of the head.
						// TODO: Future support for x and z, and possibly change to a quaternion
						// NOTE: This calculation is one frame behind 
//						if (CameraController.TrackerRotatesY == true) {
				
						Vector3 a = camera.transform.rotation.eulerAngles;
						a.x = 0; 
						a.z = 0;
						transform.parent.transform.eulerAngles = a;
//						}
						/*
			else
			{
				// We will still rotate the CameraController in the y axis
				// based on the fact that we have a Y rotation being passed 
				// in from above that still needs to take place (this functionality
				// may be better suited to be calculated one level up)
				Vector3 a = Vector3.zero;
				float y = 0.0f;
				CameraController.GetYRotation(ref y);
				a.y = y;
				gameObject.transform.parent.transform.eulerAngles = a;
			}
			*/	
						// Read shared data from CameraController	
						if (CameraController != null) {		
								if (CameraController.EnableOrientation == true) {
										CameraOrientation = Quaternion.identity;
										// Read sensor here (prediction on or off)
//										if (CameraController.PredictionOn == false)
//												OVRDevice.GetOrientation (0, ref CameraOrientation);
//										else
//												OVRDevice.GetPredictedOrientation (0, ref CameraOrientation);
								}
						}
			
						// This needs to go as close to reading Rift orientation inputs
//						OVRDevice.ProcessLatencyInputs ();			
				}
		
				// Calculate the rotation Y offset that is getting updated externally
				// (i.e. like a controller rotation)
				float yRotation = 0.0f;
				float xRotation = 0.0f;
				CameraController.GetYRotation (ref yRotation);
//				Debug.Log (xRotation);
				CameraController.GetXRotation (ref xRotation);
//				Debug.Log (xRotation);
				q = Quaternion.Euler (xRotation, yRotation, 0.0f);
				dir = q * Vector3.forward;
				q.SetLookRotation (dir, Vector3.up);
		
				// Multiply the camera controllers offset orientation (allow follow of orientation offset)
				Quaternion orientationOffset = Quaternion.identity;
				CameraController.GetOrientationOffset (ref orientationOffset);
				q = orientationOffset * q;
		
				// Multiply in the current HeadQuat (q is now the latest best rotation)
				if (CameraController != null)
						q = q * CameraOrientation;
		
				// * * *
				// Update camera rotation
				camera.transform.rotation = q;

				CameraOrientation = q;

				// * * *
				// Update camera position (first add Offset to parent transform)
				camera.transform.position = 
			camera.transform.parent.transform.position + NeckPosition;
		
				// Adjust neck by taking eye position and transforming through q
				camera.transform.position += q * EyePosition;		
		}
	
		// LatencyTest
		void LatencyTest (RenderTexture dest)
		{
		}

	#endregion

}
