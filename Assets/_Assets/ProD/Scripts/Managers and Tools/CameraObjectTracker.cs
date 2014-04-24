using UnityEngine;
using System.Collections;

namespace ProD
{
	public class CameraObjectTracker : MonoBehaviour 
	{
		public Transform Target;
		public bool AutomaticalTracking;
		
		#region 2-D
		public float screenResolution = 1024;
		public float screenSizeMultiplier = 64f;
		#endregion
		
		#region 3-D
		public float camera_Y = 16;
		public float camera_farClipPane = 30;
		public float camera_fieldOfView = 70;
		#endregion
		
		void Awake()
		{
			SetCameraToPixelPerfect(screenSizeMultiplier);	
		}
		
		//This is used for 3D prefabs.
		public void AdjustCameraForPerspectiveCoverage()
		{
			camera.isOrthoGraphic = false;
			camera.transform.position = new Vector3(camera.transform.position.x, camera_Y, camera.transform.position.z);
			camera.farClipPlane = camera_farClipPane;
			camera.fieldOfView = camera_fieldOfView;
		}
		
		public void SetCameraToPixelPerfect()
		{
			SetCameraToPixelPerfect(screenSizeMultiplier);
		}
		public void SetCameraToPixelPerfect(float cameraSize)
		{
			camera.orthographicSize = screenResolution / cameraSize;	
		}
		
		public void SetTarget(Transform target)
		{
			this.Target = target;
			SetCameraToPosition(target.position);
		}
		
		public void SetCameraToPosition(Vector3 newPosition)
		{
			newPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z);
			SetCameraOn(newPosition);
		}
		
		public void Update()
		{
			if (AutomaticalTracking && Target != null)
				SetCameraToPosition(Target.position);
		}
		
		private void SetCameraOn(Vector3 newPosition)
		{
			transform.position = newPosition;	
		}
	}
}
