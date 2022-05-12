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

        public Vector3 AngularVelocity { get; protected set; }

        [SerializeField] bool selfAsRef;
        [SerializeField] Transform axisRef;
        [SerializeField] bool OffGround;

        private void Awake()
        {
            axisRef = selfAsRef || axisRef == null ? transform : axisRef;
            StartCoroutine(VelocityUpdate());
            StartCoroutine(AngularVelocityUpdate());
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
        Vector3 GetAngularVelocity(Quaternion foreLastFrameRotation, Quaternion lastFrameRotation, float deltaTime)
        {
            var q = lastFrameRotation * Quaternion.Inverse(foreLastFrameRotation);
            // no rotation?
            // You may want to increase this closer to 1 if you want to handle very small rotations.
            // Beware, if it is too close to one your answer will be Nan
            if (Mathf.Abs(q.w) > 1023.5f / 1024.0f)
            {
                return new Vector3(0, 0, 0);
            }

            float gain;
            // handle negatives, we could just flip it but this is faster
            if (q.w < 0.0f)
            {
                var angle = Mathf.Acos(-q.w);
                gain = -2.0f * angle / (Mathf.Sin(angle) * deltaTime);
            }
            else
            {
                var angle = Mathf.Acos(q.w);
                gain = 2.0f * angle / (Mathf.Sin(angle) * deltaTime);
            }
            return new Vector3(q.x * gain, q.y * gain, q.z * gain);
        }
        IEnumerator AngularVelocityUpdate()
        {
            Quaternion prevRotation = Rotation;
            WaitForSeconds waitForSeconds = new WaitForSeconds(deltaUpdate);
            yield return waitForSeconds;
            while (true)
            {
                AngularVelocity = GetAngularVelocity(prevRotation, Rotation, deltaUpdate);
                AngularVelocity = axisRef.InverseTransformDirection(AngularVelocity);
                prevRotation = Rotation;
                yield return waitForSeconds;
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