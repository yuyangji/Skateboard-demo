using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
namespace XR_Gestures
{
    /// <summary>
    ///     A script to help test and debug a functionsContainer
    /// </summary>

    public class UnitTester : MonoBehaviour
    {
        [SerializeField] XRAvatar avatar;
        [BoxGroup("")]

        [SerializeField] FunctionsContainer container;


        [BoxGroup("Vars")]
        [SerializeField] TrackerLabel mainTracker;

        [BoxGroup("Vars")]
        [ReadOnly]
        [SerializeField]
        State state;

        [SerializeField] Debugger debugger;

        Dictionary<string, object> data;
        private void Start()
        {
            debugger = new Debugger();
            data = new Dictionary<string, object>();
            data.Add(DataKeyConstants.MainTracker, avatar.GetTracker(mainTracker));
            data.Add(DataKeyConstants.Avatar, avatar);
            data.Add(DataKeyConstants.Debugger, debugger);

            container.Initialise(data);

        }


        private void Update()
        {
            if (container == null)
            {
                return;
            }

            container.DebugRun();
            state = container.Run().state;

        }
    }
}

