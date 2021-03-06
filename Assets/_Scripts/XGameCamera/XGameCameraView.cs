﻿#region

using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger.XGameEvent;
using Assets._Scripts.XGameWorld;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameCamera {
    public class XGameCameraView : XGameView<XGameModel> {
        #region Fields

        public Transform _target;
        // The distance in the x-z plane to the target
        public float distance = 8.0f;
        // the height we want the camera to be above the target
        public float height = 8.0f;
        // How much we 
        public float heightDamping = 2.0f;
        public float rotationDamping = 3.0f;

        #endregion

        #region Instance Methods

        public override void InitEvents() {
            XGameWorldEventDispatcher.instance.addEventListener(XGameEventType.Character_Created, InitCamera);
        }

        protected void Awake() {
            InitEvents();
        }

        private void InitCamera(XGameEvent e) {
            GameObject cha = GameObject.FindGameObjectWithTag("Player");
            if (cha)
                _target = cha.transform;
        }

        protected void LateUpdate() {
            if (!_target)
                return;

            // Calculate the current rotation angles
            const float wantedRotationAngle = 45f; // _target.eulerAngles.y;
            float wantedHeight = _target.position.y + height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle,
                rotationDamping*Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping*Time.deltaTime);

            // Convert the angle into a rotation
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = _target.position;
            transform.position -= currentRotation*Vector3.forward*distance;

            // Set the height of the camera
            Vector3 position = transform.position;
            position.y = currentHeight;
            transform.position = position;

            // Always look at the target
            transform.LookAt(_target);
        }

        // Use this for initialization
        protected void Start() {
        }

        // Update is called once per frame
        protected void Update() {
        }

        #endregion
    }
}