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


        [SerializeField] FunctionObject activeFunction;

        //Debugging purposes.
        [ReadOnly]
        [SerializeField]
        public State state;

        bool active;
        public override void Reset()
        {
            currentOutput = Output.None;
            active = false;
        }
        public override void Initialise(XRAvatar _avatar)
        {
            base.Initialise(_avatar);
            var args = new FunctionArgs(_avatar, _mainTracker);

            functions.Initialise(args);

        }

        bool IsActive()
        {
            return activeFunction == null ? false: activeFunction.Run(Output.None).state == State.Performing;
        }


        public override State Run()
        {
            currentOutput = functions.Run();
            state = currentOutput.state;
            if (currentOutput.state == State.Performing && currentOutput.session == null)
            {
                currentOutput.StartSession(_mainTracker);
                active = true;
            }
            if (currentOutput.state == State.Stopped)
            {
                if (IsActive())
                    return State.Performing;
                else
                {
                    Reset();
                }
                
            }


            return state;
        }



    }


}

