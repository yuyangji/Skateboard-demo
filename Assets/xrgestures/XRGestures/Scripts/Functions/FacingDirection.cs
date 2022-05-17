using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace XR_Gestures
{
    [Serializable]
    public class FacingDirection : FunctionObject
    {

        [SerializeField] Vector3 direction;

        public bool useCustomRef;
        [ShowIf("useCustomRef")]
        [AllowNesting]
        [SerializeField] Transform refMatrix;


        [Range(0, 1)]
        [SerializeField] float tolerance;
        [SerializeField] bool useCustomTracker;

        [ShowIf("useCustomTracker")]
        [AllowNesting]
        [SerializeField] TrackerLabel trackerLabel;

        Tracker tracker;
        public override void Initialise(Dictionary<string, object> _data)
        {
            base.Initialise(_data);
            tracker = useCustomTracker ? avatar.GetTracker(trackerLabel) : mainTracker;
            refMatrix = useCustomRef ? refMatrix : avatar.GetRootReference();
        }

        protected override bool Function()
        {
            Vector3 transformedDir = refMatrix.InverseTransformDirection(tracker.transform.forward);
            return Vector3.Dot(transformedDir, direction) > 1f - tolerance;
        }

    }
}