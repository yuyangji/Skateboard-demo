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

        Tracker tracker;

        [SerializeField][ReadOnly] float speed;

        public override void OnDebug()
        {
            base.OnDebug();
            speed = tracker.Velocity.magnitude;
        }

        public override void Initialise(FunctionArgs _args)
        {
            tracker = _args.mainTracker;
        }
        protected override bool Function()
        => mode switch
        {
            Mode.MoreThan => tracker.Velocity.magnitude >= minSpeed,
            Mode.LessThan => tracker.Velocity.magnitude < minSpeed,
            Mode.Between => tracker.Velocity.magnitude >= minSpeed && tracker.Velocity.magnitude <= maxSpeed,
            _ => false,
        };

    }

}
