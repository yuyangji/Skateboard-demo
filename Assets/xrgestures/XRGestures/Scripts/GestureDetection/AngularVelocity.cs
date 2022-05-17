using System;
using UnityEngine;

namespace XR_Gestures
{
    [Serializable]
    public class AngularVelocity : FunctionObject
    {

        [SerializeField] Vector3 normalizedAngularVelocity;

        [SerializeField] float triggerSpeed;

        [Range(0.05f, 1f)]
        [SerializeField] float correction;

        [SerializeField] float dotProduct;
        /*        [SerializeField] Vector3 angularVelocity;
                [SerializeField] Vector3 angularVelocityNormalized;*/
        public override void DebugRun()
        {
            base.DebugRun();
            dotProduct = CalculateDotProduct();
            /*            angularVelocity = _tracker.AngularVelocity;
                        angularVelocityNormalized = _tracker.AngularVelocity.normalized;*/

        }
        float CalculateDotProduct()
        {
            return Vector3.Dot(mainTracker.AngularVelocity.normalized, normalizedAngularVelocity);
        }
        protected override bool Function()
        {
            float res = CalculateDotProduct();
            return res > 1f - correction && mainTracker.AngularVelocity.magnitude > triggerSpeed;

        }


    }

}
