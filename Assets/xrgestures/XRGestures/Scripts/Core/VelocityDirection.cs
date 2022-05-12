using System;
using UnityEngine;
using NaughtyAttributes;
namespace XR_Gestures
{
    [Serializable]
    public class VelocityDirection : FunctionObject
    {
        [SerializeField] float correction;

        [SerializeField] Vector3 direction;

        [SerializeField][ReadOnly] Vector3 _direction;
        [SerializeField][ReadOnly] float dotProduct;

        Tracker tracker;

        public override void Initialise(FunctionArgs _args)
        {
            tracker = _args.mainTracker;
            direction = direction.normalized;
        }

        public override void DebugRun()
        {
            Debug.DrawLine(tracker.Position, tracker.Position + GetDirection());
            _direction = tracker.Velocity.normalized;
            dotProduct = Vector3.Dot(tracker.Velocity.normalized, direction);
            // Debug.Log(tracker.Velocity);
        }

        protected Vector3 GetDirection()
        => tracker.TransformToRefDir(direction).normalized;
        protected override bool Function()
        {
            float res = Vector3.Dot(tracker.Velocity.normalized, direction);

            return res >= 1f - correction;
        }


    }

}