using System.Collections.Generic;
using UnityEngine;


namespace XR_Gestures
{


    public abstract class Gesture : ScriptableObject
    {
        [SerializeField] protected TrackerLabel mainTracker;
        [SerializeField] public List<Gesture> concurrentGestures;
        protected Tracker _mainTracker;

        //LISTENERS
        readonly List<IGestureListener> listeners = new List<IGestureListener>();

        public void RegisterListener(IGestureListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IGestureListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }


        public void RaiseEvent()
        {
            GestureCallback e = new GestureCallback();
            e.gesture = this;
            foreach (var listener in listeners)
            {
                listener.OnEventRaised(e);
            }
        }

        //GESTURE MANAGEMENT
        public abstract State Run();

        public virtual void Initialise(XRAvatar _avatar)
        {

            _mainTracker = _avatar.GetTracker(mainTracker);
        }
        public abstract void Reset();
    }


    public class Output
    {
        public State state;
        public TrackingSession? session;
        public Output ChangeState(State _newState)
        {
            state = _newState;
            return this;
        }

        public static Output None => new Output() { state = State.None };

        public void StartSession(Tracker _tracker)
        {
            session = new TrackingSession(_tracker, Time.time);
        }

        public void StopSession()
        {
            session = null;
        }
    }

    public readonly struct TrackingSession
    {
        public Vector3 WorldStartPos { get; }
        public Quaternion Rotation { get; }
        public Vector3 LocalStartPos { get; }
        public float StartTime { get; }
        public TrackingSession(Tracker _tracker, float _time)
        {
            WorldStartPos = _tracker.Position;
            LocalStartPos = _tracker.LocalPosition;
            Rotation = _tracker.Rotation;
            StartTime = _time;
        }

    }

    public enum State
    {
        None,
        Performed,
        Performing,
        Stopped,
        ForceStart
    }

}
