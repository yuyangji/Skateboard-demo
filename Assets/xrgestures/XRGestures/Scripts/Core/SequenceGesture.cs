using NaughtyAttributes;
using UnityEngine;

/// <summary>
///     A gesture that runs functions in a sequence. 
///     Move on to next group of functions only when current group is true.
///     If current group turns false, then stop.
///     Use cases: 
/// </summary>
namespace XR_Gestures
{
    [CreateAssetMenu(menuName = "ScriptableObject/Gesture (Sequence)")]
    public class SequenceGesture : Gesture
    {
        [SerializeField] FunctionObject activation;
        [SerializeField] FunctionsContainer functions;

        Output currentOutput = Output.None;

        [ReadOnly]
        [SerializeField] State state;
        int index;
        public override void Initialise(XRAvatar _avatar)
        {
            base.Initialise(_avatar);
            functions.Initialise(data);
            activation.Initialise(data);
            index = 0;
        }

        public override State Run()
        {
            Output output = activation.Run(Output.None);
            if (output.state != State.Stopped)
            {
                if (functions.Run().state == State.Performing)
                {
                    Reset();
                    return State.Performed;
                }

            }
            else
            {
                Reset();
            }

            state = currentOutput.state;
            return currentOutput.state;
        }



        public override void Reset()
        {

            currentOutput = Output.None;
        }

    }


}