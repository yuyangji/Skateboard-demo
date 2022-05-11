using System;

using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

namespace XR_Gestures
{

    public enum TrackerLabel
    {
        toe_L,
        toe_R,
        heel_L,
        heel_R,
        hip,
        shoulder_L,
        shoulder_R,
        elbow_L,
        elbow_R,
        hand_L,
        hand_R,
    }


    public class XRAvatar : MonoBehaviour, ISerializationCallbackReceiver
    {
        //Struct to link a tracker and its label.
        [Serializable]
        struct TrackerLink
        {
            [HideInInspector]
            public string name;
            [HideInInspector]
            public TrackerLabel label;

            [SerializeField] public Tracker tracker;
            public TrackerLink(TrackerLabel label)
            {
                this.label = label;
                this.tracker = null;
                this.name = label.ToString();
            }

        }

        [SerializeField]
        [HideInInspector]
        private List<TrackerLink> trackers;

        [SerializeField] Color textColor;
        [SerializeField] int fontSize;

        private Dictionary<TrackerLabel, Tracker> _trackersDict = new Dictionary<TrackerLabel, Tracker>();
        GUIStyle textStyle;

        //The transform that will determine which way is forward.
        [SerializeField] Transform rootReference;

        private void OnValidate()
        {   
            //Get all TrackerLabel enum values and use it create trackerLink class.
            Enum.GetValues(typeof(TrackerLabel))
                .OfType<TrackerLabel>()
                .ToList()
                .Where(t => !trackers.Select(link => link.label).Contains(t))
                .ToList()
                .ForEach(label => trackers.Add(new TrackerLink(label)));
            //Set the style of the GUI Tracker Gizmos label.
            textStyle = new GUIStyle();
            textColor.a = 1f;
            textStyle.normal.textColor = textColor;
            textStyle.fontSize = fontSize;
            textStyle.alignment = TextAnchor.MiddleCenter;

        }

        private void Awake()
        {
            trackers.ForEach(t => _trackersDict[t.label] = t.tracker);
            Invoke("Calibrate", 1f);
        }

        public Tracker GetTracker(TrackerLabel _label)
        => _trackersDict[_label];

        //Draw gizmo of markers in the scene
        private void OnDrawGizmosSelected()
        {
            if (trackers == null) return;
            trackers.ForEach(t =>
            {
                if (t.tracker != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(t.tracker.Position, 0.025f);
               
                    UnityEditor.Handles.Label(t.tracker.Position, t.name, textStyle);
                }
            });
        }
        [Button("Refresh")]
        public void Refresh()
        {
            trackers.Clear();
            OnValidate();
        }


        [Button("Calibrate")]
        public void Calibrate()
        {
            GetTracker(TrackerLabel.toe_L).CalibrateOffsets();
            GetTracker(TrackerLabel.heel_L).CalibrateOffsets();
            GetTracker(TrackerLabel.toe_R).CalibrateOffsets();
            GetTracker(TrackerLabel.heel_R).CalibrateOffsets();

        }

        public Transform GetRootReference() => rootReference;
        public void OnBeforeSerialize()
        {
           
        }

        public void OnAfterDeserialize()
        {
            if(_trackersDict == null) _trackersDict = new Dictionary<TrackerLabel, Tracker>();
            trackers.ForEach(t => _trackersDict[t.label] = t.tracker);
        }
    }


}


