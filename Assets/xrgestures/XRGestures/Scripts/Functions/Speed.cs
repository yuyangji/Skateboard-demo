using NaughtyAttributes;
using System;
using UnityEngine;

namespace XR_Gestures
{
    [Serializable]
    public class Speed : FunctionObject
    {
        enum Mode { MoreThan, LessThan, Between }

        [SerializeField] float minSpeed;

        [ShowIf("mode", Mode.Between)]
        [AllowNesting]
        [SerializeField] float maxSpeed;

        [SerializeField] Mode mode;

        [SerializeField] bool customTracker;
        [ShowIf("customTracker")]
        [AllowNesting]
        [SerializeField] TrackerLabel trackerLabel;

        public override void DebugRun()
        {
            base.DebugRun();
            debugger.AddValue("speed", mainTracker.Velocity.magnitude.ToString());
        }

        protected override bool Function()
        => mode switch
        {
            Mode.MoreThan => mainTracker.Velocity.magnitude >= minSpeed,
            Mode.LessThan => mainTracker.Velocity.magnitude < minSpeed,
            Mode.Between => mainTracker.Velocity.magnitude >= minSpeed && mainTracker.Velocity.magnitude <= maxSpeed,
            _ => false,
        };

    }

}
