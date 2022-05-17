using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace XR_Gestures
{
    public static class DataKeyConstants
    {
        public const string MainTracker = "MainTracker";
        public const string Avatar = "Avatar";
        public const string Debugger = "Debugger";

    }

    [Serializable]
    public class Debugger
    {
        [ResizableTextArea]
        [AllowNesting]
        [ReadOnly]
        public string debugText;

        public void AddText(string text)
        {
            if (String.IsNullOrEmpty(debugText))
            {
                debugText += $"{text}";
            }
            else
            {
                debugText += $"\n{text}";
            }
        }

        public void AddTitle(string _title)
        {
            if (String.IsNullOrEmpty(debugText))
            {
                debugText += $"-----{_title}-----\n";
            }
            else
            {
                debugText += $"\n\n-----{_title}-----\n";
            }
        }


        public void AddValue(string _name, string _value)
        {
            if (String.IsNullOrEmpty(debugText))
            {
                debugText += $"{_name}\t {_value}";
            }
            else
            {
                debugText += $"\n{_name}\t {_value}";
            }

        }
        public void ClearText()
        {
            debugText = "";
        }
    }


    public abstract class Gesture : ScriptableObject
    {
        [SerializeField] protected TrackerLabel mainTracker;
        [SerializeField] public List<Gesture> concurrentGestures;

        //DEBUGGING
        [SerializeField] protected bool RunDebug;
        [SerializeField] protected Debugger debugger;

        [ReadOnly]
        [SerializeField]
        public State state;

        //LISTENERS
        readonly List<IGestureListener> listeners = new List<IGestureListener>();
        protected Dictionary<string, object> data;
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
            data = new Dictionary<string, object>();
            debugger = new Debugger();
            data.Add(DataKeyConstants.MainTracker, _avatar.GetTracker(mainTracker));
            data.Add(DataKeyConstants.Avatar, _avatar);
            data.Add(DataKeyConstants.Debugger, debugger);
        }
        public abstract void ResetFunction();
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
    }

}
