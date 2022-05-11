using System;
using System.Collections;
using UnityEngine;

namespace XR_Gestures
{
    public class Tracker : MonoBehaviour
    {
        protected enum upAxis { x, y, z }

        // --- Calibration --- //
        public float offsetFromGround;
        public float groundMarginOfError = 0.03f;
        [SerializeField] float deltaUpdate = 0.1f;


        [SerializeField] protected upAxis UpAxis = upAxis.y;

        // ---- TRACKING VARS --- //
        public Vector3 Velocity { get; protected set; }

        public float deltaTime { get; protected set; }

        public float angleRotation { get; private set; }

        [SerializeField] bool selfAsRef;
        [SerializeField] Transform axisRef;
        [SerializeField] bool OffGround;

        private void Awake()
        {
            axisRef = selfAsRef || axisRef == null ? transform : axisRef;
            StartCoroutine(VelocityUpdate());
        }

        public void SetReferenceAxis(Transform _refAxis)
        {
            axisRef = _refAxis;
        }

        public Vector3 TransformToRefDir(Vector3 _v)
        => axisRef.TransformDirection(_v);

        IEnumerator VelocityUpdate()
        {

            Vector3 prevPos = transform.position;
            yield return new WaitForSeconds(deltaUpdate);
            while (true)
            {

                // refT.worldToLocalMatrix.MultiplyVector (Direction)
                var _vel = axisRef.InverseTransformDirection(Position - prevPos) / deltaUpdate;

                Velocity = _vel.RoundTo3();

                prevPos = transform.position;

                yield return new WaitForSeconds(deltaUpdate);
            }
        }



        //This is for foot only. TODO: Generalise so that any tracker can detect if its on the ground.
        public bool IsOnGround()
        => OffGround ? false :
            UpAxis switch
            {
                upAxis.x => transform.position.x <= offsetFromGround + groundMarginOfError,
                upAxis.y => transform.position.y <= offsetFromGround + groundMarginOfError,
                upAxis.z => transform.position.z <= offsetFromGround + groundMarginOfError,
                _ => throw new NotImplementedException()
            };


        public void CalibrateOffsets()
        {
            offsetFromGround = (UpAxis == upAxis.y) ? transform.position.y :
                               (UpAxis == upAxis.z) ? transform.position.z :
                               transform.position.x;

        }


        public Vector3 Position => transform.position;
        public Vector3 LocalPosition => transform.localPosition;

        public Quaternion Rotation => transform.rotation;

    }

}