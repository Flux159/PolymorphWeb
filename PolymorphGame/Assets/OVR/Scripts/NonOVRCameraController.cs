using UnityEngine;
using System.Collections;

public class NonOVRCameraController : OVRComponent
{

		// PRIVATE MEMBERS
		private bool   UpdateCamerasDirtyFlag = false;	
		private Camera CameraLeft, CameraRight = null;
		private float  LensOffsetLeft, LensOffsetRight = 0.0f;  // normalized screen space
		private float  AspectRatio = 1.0f;						
		private float  DistK0, DistK1, DistK2, DistK3 = 0.0f; 	// lens distortion parameters
	
		// Initial orientation of the camera, can be used to always set the zero orientation of
		// the cameras to follow a set forward facing orientation.
		private Quaternion OrientationOffset = Quaternion.identity;	
		// Set Y rotation here; this will offset the y rotation of the cameras. 
		private float   YRotation = 0.0f;
		private float 	XRotation = 0.0f;

		// PUBLIC MEMBERS
	
		// IPD
		[SerializeField]
		private float
				ipd = 0.064f; 				// in millimeters
		public 	float 		IPD {
				get{ return ipd;}
				set {
						ipd = value;
						UpdateCamerasDirtyFlag = true;
				}
		}
	
		// VERTICAL FOV
		[SerializeField]
		private float
				verticalFOV = 90.0f;	 			// in degrees
		public 	float		VerticalFOV {
				get{ return verticalFOV;}
				set {
						verticalFOV = value;
						UpdateCamerasDirtyFlag = true;
				}
		}
	
		// Camera positioning:
		// CameraRootPosition will be used to calculate NeckPosition and Eye Height
		public Vector3 		CameraRootPosition = new Vector3 (0.0f, 1.0f, 0.0f);					
		// From CameraRootPosition to neck
		public Vector3 		NeckPosition = new Vector3 (0.0f, 0.7f, 0.0f);	
		// From neck to eye (rotation and translation; x will be different for each eye, based on IPD)
		public Vector3 		EyeCenterPosition = new Vector3 (0.0f, 0.15f, 0.09f);
	
		// Use player eye height as set in the Rift config tool
		public bool 		UsePlayerEyeHeight = false;
		private bool 		PrevUsePlayerEyeHeight = false;
	
		// Set this transform with an object that the camera orientation should follow.
		// NOTE: Best not to set this with the OVRCameraController IF TrackerRotatesY is
		// on, since this will lead to uncertain output
		public Transform 	FollowOrientation = null;
	
		// Set to true if we want the rotation of the camera controller to be influenced by tracker
		public bool  		TrackerRotatesY = false;
	
		public bool    		PortraitMode = false; // We currently default to landscape mode for render
		private bool 		PrevPortraitMode = false;
	
		// Use this to enable / disable Tracker orientation
		public bool         EnableOrientation = true;
		// Use this to turn on/off Prediction
		public bool			PredictionOn = true;
		// Use this to decide where tracker sampling should take place
		// Setting to true allows for better latency, but some systems
		// (such as Pro water) will break
		public bool			CallInPreRender = false;
		// Use this to turn on wire-mode
		public bool			WireMode = false;
	
		// Turn lens distortion on/off; use Chromatic Aberration in lens distortion calculation
		public bool 		LensCorrection = true;
		public bool 		Chromatic = true;
	
		// UNITY CAMERA FIELDS
		// Set the background color for both cameras
		[SerializeField]
		private Color
				backgroundColor = new Color (0.192f, 0.302f, 0.475f, 1.0f);
		public  Color       BackgroundColor {
				get{ return backgroundColor;}
				set {
						backgroundColor = value;
						UpdateCamerasDirtyFlag = true;
				}
		}
	
		// Set the near and far clip plane for both cameras
		[SerializeField]
		private float
				nearClipPlane = 0.15f;
		public  float 		NearClipPlane {
				get{ return nearClipPlane;}
				set {
						nearClipPlane = value;
						UpdateCamerasDirtyFlag = true;
				}
		}
	
		[SerializeField]
		private float
				farClipPlane = 1000.0f;  
		public  float 		FarClipPlane {
				get{ return farClipPlane;}
				set {
						farClipPlane = value;
						UpdateCamerasDirtyFlag = true;
				}
		}

		// Awake
		new void Awake ()
		{
				base.Awake ();
		
				// Get the cameras
				Camera[] cameras = gameObject.GetComponentsInChildren<Camera> ();

				CameraLeft = cameras [0];
				CameraRight = cameras [0];

//				for (int i = 0; i < cameras.Length; i++) {
//						if (cameras [i].name == "CameraLeft")
//								CameraLeft = cameras [i];
//			
//						if (cameras [i].name == "CameraRight")
//								CameraRight = cameras [i];
//				}
//		
				if ((CameraLeft == null) || (CameraRight == null))
						Debug.LogWarning ("WARNING: Unity Cameras in OVRCameraController not found!");
		}
	
		// Start
		new void Start ()
		{
				base.Start ();
		
				// Get the required Rift infromation needed to set cameras
//				InitCameraControllerVariables ();
		
				// Initialize the cameras
//				UpdateCamerasDirtyFlag = true;
//				UpdateCameras ();
		
//				SetMaximumVisualQuality ();
		
		}

		// Update 
		new void Update ()
		{
				base.Update ();		
				UpdateCameras ();
		}

		// InitCameras
		void UpdateCameras ()
		{
				// Values that influence the stereo camera orientation up and above the tracker
				if (FollowOrientation != null)
						OrientationOffset = FollowOrientation.rotation;
		
				// Handle Portrait Mode changes to cameras
//				SetPortraitMode ();
		
				// Handle positioning of eye height and other things here
//				UpdatePlayerEyeHeight ();

				UpdateCamerasDirtyFlag = true;

				// Handle all other camera updates here
				if (UpdateCamerasDirtyFlag == false)
						return;
		
				float distOffset = 0.5f + (LensOffsetLeft * 0.5f);
				float perspOffset = LensOffsetLeft;
				float eyePositionOffset = -IPD * 0.5f;
//				ConfigureCamera (ref CameraLeft, distOffset, perspOffset, eyePositionOffset);
		
				distOffset = 0.5f + (LensOffsetRight * 0.5f);
				perspOffset = LensOffsetRight;
				eyePositionOffset = IPD * 0.5f;
//				ConfigureCamera (ref CameraRight, distOffset, perspOffset, eyePositionOffset);
		
				UpdateCamerasDirtyFlag = false;
		}



		///////////////////////////////////////////////////////////
		// PUBLIC FUNCTIONS
		///////////////////////////////////////////////////////////
	
		// SetCameras - Should we want to re-target the cameras
		public void SetCameras (ref Camera cameraLeft, ref Camera cameraRight)
		{
				CameraLeft = cameraLeft;
				CameraRight = cameraRight;
				UpdateCamerasDirtyFlag = true;
		}
	
		// Get/SetIPD 
		public void GetIPD (ref float ipd)
		{
				ipd = IPD;
		}
		public void SetIPD (float ipd)
		{
				IPD = ipd;
				UpdateCamerasDirtyFlag = true;
		
		}
	
		//Get/SetVerticalFOV
		public void GetVerticalFOV (ref float verticalFOV)
		{
				verticalFOV = VerticalFOV;
		}
		public void SetVerticalFOV (float verticalFOV)
		{
				VerticalFOV = verticalFOV;
				UpdateCamerasDirtyFlag = true;
		}
	
		//Get/SetAspectRatio
		public void GetAspectRatio (ref float aspecRatio)
		{
				aspecRatio = AspectRatio;
		}
		public void SetAspectRatio (float aspectRatio)
		{
				AspectRatio = aspectRatio;
				UpdateCamerasDirtyFlag = true;
		}
	
		// Get/SetDistortionCoefs
		public void GetDistortionCoefs (ref float distK0, 
	                                ref float distK1, 
	                                ref float distK2, 
	                                ref float distK3)
		{
				distK0 = DistK0;
				distK1 = DistK1;
				distK2 = DistK2;
				distK3 = DistK3;
		}
		public void SetDistortionCoefs (float distK0, 
	                                float distK1, 
	                                float distK2, 
	                                float distK3)
		{
				DistK0 = distK0;
				DistK1 = distK1;
				DistK2 = distK2;
				DistK3 = distK3;
				UpdateCamerasDirtyFlag = true;
		}
	
		// Get/Set CameraRootPosition
		public void GetCameraRootPosition (ref Vector3 cameraRootPosition)
		{
				cameraRootPosition = CameraRootPosition;
		}
		public void SetCameraRootPosition (ref Vector3 cameraRootPosition)
		{
				CameraRootPosition = cameraRootPosition;
				UpdateCamerasDirtyFlag = true;
		}
	
		// Get/SetNeckPosition
		public void GetNeckPosition (ref Vector3 neckPosition)
		{
				neckPosition = NeckPosition;
		}
		public void SetNeckPosition (Vector3 neckPosition)
		{
				// This is locked to the NeckPosition that is set by the
				// Player profile.
				if (UsePlayerEyeHeight != true) {
						NeckPosition = neckPosition;
						UpdateCamerasDirtyFlag = true;
				}
		}
	
		// Get/SetEyeCenterPosition
		public void GetEyeCenterPosition (ref Vector3 eyeCenterPosition)
		{
				eyeCenterPosition = EyeCenterPosition;
		}
		public void SetEyeCenterPosition (Vector3 eyeCenterPosition)
		{
				EyeCenterPosition = eyeCenterPosition;
				UpdateCamerasDirtyFlag = true;
		}
	
		// Get/SetOrientationOffset
		public void GetOrientationOffset (ref Quaternion orientationOffset)
		{
				orientationOffset = OrientationOffset;
		}
		public void SetOrientationOffset (Quaternion orientationOffset)
		{
				OrientationOffset = orientationOffset;
		}
	
		// Get/SetYRotation
		public void GetYRotation (ref float yRotation)
		{
				yRotation = YRotation;
		}
		public void SetYRotation (float yRotation)
		{
				YRotation = yRotation;
		}
	
		public void GetXRotation (ref float xRotation)
		{
				xRotation = XRotation;
		}
	
		public void SetXRotation (float xRotation)
		{
				XRotation = xRotation;
		}
	
		// Get/SetTrackerRotatesY
		public void GetTrackerRotatesY (ref bool trackerRotatesY)
		{
				trackerRotatesY = TrackerRotatesY;
		}
		public void SetTrackerRotatesY (bool trackerRotatesY)
		{
				TrackerRotatesY = trackerRotatesY;
		}
	
		// Camera orientation and position
	
		// GetCameraOrientationEulerAngles
		public bool GetCameraOrientationEulerAngles (ref Vector3 angles)
		{
				if (CameraRight == null)
						return false;
		
				angles = CameraRight.transform.rotation.eulerAngles;
				return true;
		}
	
		// GetCameraOrientation
		public bool GetCameraOrientation (ref Quaternion quaternion)
		{
				if (CameraRight == null)
						return false;
		
				quaternion = CameraRight.transform.rotation;
				return true;
		}
	
		// GetCameraPosition
		public bool GetCameraPosition (ref Vector3 position)
		{
				if (CameraRight == null)
						return false;
		
				position = CameraRight.transform.position;
		
				return true;
		}
	
		// Access camera
	
		// GetCamera
		public void GetCamera (ref Camera camera)
		{
				camera = CameraRight;
		}
	
		// AttachGameObjectToCamera
		public bool AttachGameObjectToCamera (ref GameObject gameObject)
		{
				if (CameraRight == null)
						return false;
		
				gameObject.transform.parent = CameraRight.transform;
		
				return true;
		}
	
		// DetachGameObjectFromCamera
		public bool DetachGameObjectFromCamera (ref GameObject gameObject)
		{
				if ((CameraRight != null) && (CameraRight.transform == gameObject.transform.parent)) {
						gameObject.transform.parent = null;
						return true;
				}				
		
				return false;
		}
	
		// Get Misc. values from CameraController
	
		// GetPlauerEyeHeight
		public bool GetPlayerEyeHeight (ref float eyeHeight)
		{
				eyeHeight = CameraRootPosition.y + NeckPosition.y + EyeCenterPosition.y;  
		
				return true;
		}
	
		// SetMaximumVisualQuality
		public void SetMaximumVisualQuality ()
		{
				QualitySettings.softVegetation = true;
				QualitySettings.maxQueuedFrames = 0;
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
				QualitySettings.vSyncCount = 1;
		}


}
