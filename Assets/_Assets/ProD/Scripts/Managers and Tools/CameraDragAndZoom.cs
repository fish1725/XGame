//This is the script that's currently being used for the Webplayer version online.
//We included this so users who fancy an in-game minimap may benefit from this example.

using UnityEngine;

namespace ProD
{
	public class CameraDragAndZoom : MonoBehaviour //a.k.a MiniMap.cs
	{
		private Camera _Camera;
	    public float dragSpeed = -1f;
		public float zoomSpeed = 1f;
		public bool useLimit_x = false;
		private float limit_X = 512;
		private Vector3 _CameraPos;
		private float _Mouse_X;
		private	float _Mouse_Y;
		
		void Awake()
		{
			_Camera = gameObject.GetComponent<Camera>();
		}
		
		public void SetLimit(bool b)
		{
			useLimit_x = b;	
		}
		
	 	void Update()
	    {
			if(useLimit_x && Input.mousePosition.x > limit_X) return;
			
			//Zoom in and out with scrollwheel
			if (Input.GetAxis("Mouse ScrollWheel") < 0) //Backwardsscroll.
			{
				_Camera.orthographicSize = _Camera.orthographicSize + (1*zoomSpeed);
			}
			else if (Input.GetAxis("Mouse ScrollWheel") > 0) //Forwardscroll.
	        {
	            _Camera.orthographicSize = _Camera.orthographicSize - (1*zoomSpeed);
	        }
			
			//Click and drag the map
			if (!Input.GetMouseButton(0)) return;
			_CameraPos = gameObject.transform.position;
			_Mouse_X = Input.GetAxis("Mouse X");
			_Mouse_Y = Input.GetAxis("Mouse Y");
			_CameraPos = new Vector3(_Mouse_X * dragSpeed, 0, _Mouse_Y * dragSpeed);
			camera.transform.position += _CameraPos;
		}
	}
}