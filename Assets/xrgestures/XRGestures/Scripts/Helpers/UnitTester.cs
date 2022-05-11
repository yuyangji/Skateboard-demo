using NaughtyAttributes;
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

        private void Start()
        {
            FunctionArgs args = new FunctionArgs(avatar, avatar.GetTracker(mainTracker));
            container.Initialise(args);

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

