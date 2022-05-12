using NaughtyAttributes;
using UnityEngine;
namespace XR_Gestures
{
    [CreateAssetMenu(menuName = "ScriptableObject/Gesture (Continuous)")]
    public class ContinuousGesture : Gesture
    {

        [HorizontalLine]

        [ShowIf("NeverShow")]
        [SerializeField] public FunctionsContainer functions;

        bool NeverShow => false;

        Output currentOutput = Output.None;


        [SerializeReference, SubclassSelector] FunctionObject activeFunction;

        //Debugging purposes.
        [ReadOnly]
        [SerializeField]
        public State state;

        bool wasPerforming;

        [SerializeField] bool RunDebug;
        public override void Reset()
        {
            currentOutput = Output.None;
            wasPerforming = false;
        }
        public override void Initialise(XRAvatar _avatar)
        {
            base.Initialise(_avatar);
            var args = new FunctionArgs(_avatar, _mainTracker);

            functions.Initialise(args);
            if (activeFunction != null)
            {
                activeFunction.Initialise(args);
            }
        }

        bool IsActive()
        {
            return activeFunction == null ? false : activeFunction.Run(Output.None).state == State.Performing;
        }


        public override State Run()
        {
            if (RunDebug)
            {
                functions.DebugRun();
            }

            currentOutput = functions.Run();
            state = currentOutput.state;
            if (currentOutput.state == State.Performing && currentOutput.session == null)
            {
                currentOutput.StartSession(_mainTracker);
                wasPerforming = true;
            }
            if (currentOutput.state == State.Stopped)
            {
                if (wasPerforming && IsActive())
                {
                    return State.Performing;
                }
                else
                {
                    Reset();
                }

            }


            return state;
        }



    }


}

