using NaughtyAttributes;
using System.Diagnostics;
using UnityEngine;
namespace XR_Gestures
{
    [CreateAssetMenu(menuName = "ScriptableObject/Gesture (Continuous)")]
    public class ContinuousGesture : Gesture
    {

        [HorizontalLine]

        [ShowIf("NeverShow")]
        [SerializeField] public FunctionsContainer functions = new FunctionsContainer();

        bool NeverShow => false;

        Output currentOutput = Output.None;


        [SerializeReference, SubclassSelector] FunctionObject activeFunction;

        //Debugging purposes.


        [SerializeField] float timeToActivate;

        //vars
        Stopwatch stopWatch;
        bool wasPerforming;


        public override void ResetFunction()
        {
            currentOutput = Output.None;
            wasPerforming = false;
            stopWatch.Reset();
        }
        public override void Initialise(XRAvatar _avatar)
        {
            base.Initialise(_avatar);
            stopWatch = new Stopwatch();

            functions.Initialise(data);
            if (activeFunction != null)
            {
                activeFunction.Initialise(data);
            }
            ResetFunction();
        }

        bool IsActive()
        {
            return activeFunction == null ? false : activeFunction.Run(Output.None).state == State.Performing;
        }

        State OnPerforming()
        {
            if (!stopWatch.IsRunning)
            {
                stopWatch.Start();
                wasPerforming = true;
                return State.Stopped;
            }
            else if (stopWatch.Elapsed.TotalSeconds > timeToActivate)
            {
                wasPerforming = true;
                return State.Performing;
            }
            return State.Stopped;

        }
        State OnStopped()
        {
            if (wasPerforming && IsActive())
            {
                return State.Performing;
            }
            else
            {
                ResetFunction();
                return State.Stopped;
            }
        }

        public override State Run()
        {
            if (RunDebug)
            {
                debugger.ClearText();
                functions.DebugRun();

            }

            currentOutput = functions.Run();
            state = currentOutput.state;

            switch (state)
            {
                case State.Performing:
                    return OnPerforming();
                case State.Stopped:
                    return OnStopped();
            }
            return state;
        }



    }


}

