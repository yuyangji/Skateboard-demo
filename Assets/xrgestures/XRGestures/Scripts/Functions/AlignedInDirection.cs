using System;
using UnityEngine;

namespace XR_Gestures
{
    [Serializable]
    public class AlignedInDirection : FunctionObject
    {
        [SerializeField] Transform refMatrix;
        [SerializeField] TrackerLabel refTrackerLabel;

        [SerializeField] public Vector3 direction; //public for pose recorder
        [SerializeField] TrackerLabel fromLabel;
        [SerializeField] TrackerLabel toLabel;

        Tracker from, to;

        [Range(0, 1)]
        [SerializeField] float tolerance;

        public override void DebugRun()
        {
            Debug.DrawLine(from.Position, from.Position + direction.normalized);
            Debug.DrawLine(from.Position, from.Position + GetCurrentDir(), Color.green);
        }

        public Vector3 GetCurrentDir()
        {
            return refMatrix.InverseTransformDirection((to.Position - from.Position).normalized).normalized;
        }

        public override void Initialise(FunctionArgs _args)
        {
            from = _args.avatar.GetTracker(fromLabel);
            to = _args.avatar.GetTracker(toLabel);
            if (refMatrix == null)
            {
                refMatrix = _args.avatar.GetTracker(refTrackerLabel).transform;
            }
        }

        protected override bool Function()
        {
            return Vector3.Dot(GetCurrentDir(), direction.normalized) > 1f - tolerance;
        }


    }
}