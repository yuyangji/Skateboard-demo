using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
namespace XR_Gestures
{
    [Serializable]
    public class FacingDirection : FunctionObject
    {
        
        [SerializeField] Vector3 direction;

        public bool useCustomRef;
        [ShowIf("useCustomRef")][AllowNesting]
        [SerializeField] Transform refMatrix;


        [Range(0, 1)]
        [SerializeField] float tolerance;
        [SerializeField] bool useCustomTracker;

        [ShowIf("useCustomTracker")][AllowNesting]
        [SerializeField] TrackerLabel trackerLabel;

        Tracker tracker;
        public override void Initialise(FunctionArgs _args)
        {
            tracker = useCustomTracker ? _args.avatar.GetTracker(trackerLabel) : _args.mainTracker;
            refMatrix = useCustomRef ? refMatrix : _args.avatar.GetRootReference();
        }

        protected override bool Function()
        {
            Vector3 transformedDir = refMatrix.InverseTransformDirection(tracker.transform.forward);
            return Vector3.Dot(transformedDir, direction) > 1f - tolerance;
        }

    }
}