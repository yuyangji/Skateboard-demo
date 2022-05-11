using NaughtyAttributes;
using System;
using UnityEngine;

namespace XR_Gestures
{


    [Serializable]
    public class FeetPose : FunctionObject
    {
        enum Observation { Left, Right, Both }
        //Which foot to observe.
        [SerializeField] Observation observation;

        //The states the foot has to be in. Will only use foot based on observation.
        [ShowIf("showLeft")]
        [AllowNesting]
        [SerializeField] FootState leftState;

        [ShowIf("showRight")]
        [AllowNesting]
        [SerializeField] FootState rightState;

        public bool showLeft()
        => observation == Observation.Left || observation == Observation.Both;

        public bool showRight()
        => observation == Observation.Right || observation == Observation.Both;

        Tracker toeL, heelL, toeR, heelR;

        [ReadOnly]
        [SerializeField] string debugState;


        public override void OnDebug()
        {
            base.OnDebug();
            debugState = Function().ToString();

        }


        public override void Initialise(FunctionArgs _args)
        {
            toeL = _args.avatar.GetTracker(TrackerLabel.toe_L);
            heelL = _args.avatar.GetTracker(TrackerLabel.heel_L);
            toeR = _args.avatar.GetTracker(TrackerLabel.toe_R);
            heelR = _args.avatar.GetTracker(TrackerLabel.heel_R);
        }
        protected override bool Function()
        {
            return observation switch
            {
                Observation.Left => IsState(toeL, heelL, leftState),
                Observation.Right => IsState(toeR, heelR, rightState),
                _ => IsState(toeL, heelL, leftState) && IsState(toeR, heelR, rightState)
            };
        }

        bool IsState(Tracker _toe, Tracker _heel, FootState _state)
        {
            return _state switch
            {
                FootState.HeelRaise => _toe.IsOnGround() && !_heel.IsOnGround(),
                FootState.ToeRaise => !_toe.IsOnGround() && _heel.IsOnGround(),
                FootState.Flat => _toe.IsOnGround() && _heel.IsOnGround(),
                FootState.Lifted => !_toe.IsOnGround() && !_heel.IsOnGround(),
                FootState.ToeGrounded => _toe.IsOnGround(),
                FootState.HeelGrounded => _heel.IsOnGround(),
                _ => false
            };
        }
    }
    public enum FootState
    {
        ToeRaise,
        HeelRaise,
        Flat,
        Lifted,
        ToeGrounded,
        HeelGrounded
    }


}