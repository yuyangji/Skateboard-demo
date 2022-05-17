using System;
using System.Collections.Generic;
using UnityEngine;
namespace XR_Gestures
{
    [Serializable]
    public class VelocityDirection : FunctionObject
    {
        [SerializeField] float correction;

        [SerializeField] Vector3 direction;

        public override void Initialise(Dictionary<string, object> _data)
        {
            base.Initialise(_data);
            direction = direction.normalized;
        }

        public override void DebugRun()
        {
            Debug.DrawLine(mainTracker.Position, mainTracker.Position + GetDirection());

            debugger.AddValue("direction", mainTracker.Velocity.normalized.ToString());
            debugger.AddValue("dotProduct", DotProduct().ToString());

            // Debug.Log(tracker.Velocity);
        }

        float DotProduct()
        {
            return Vector3.Dot(mainTracker.Velocity.normalized, direction);
        }


        protected Vector3 GetDirection()
        => mainTracker.TransformToRefDir(direction).normalized;
        protected override bool Function()
        {
            float res = Vector3.Dot(mainTracker.Velocity.normalized, direction);

            return res >= 1f - correction;
        }


    }

}