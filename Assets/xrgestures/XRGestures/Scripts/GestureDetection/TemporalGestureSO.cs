using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;


namespace XR_Gestures
{


    [CreateAssetMenu(fileName = "NewGesture", menuName = "ScriptableObject/Gesture (Time based)")]
    public class TemporalGestureSO : Gesture
    {
        [ShowIf("NeverShow")]
        [SerializeField] List<FunctionsContainer> functions;
        [SerializeField] DurationMode mode;

        [Tooltip("Previous function must remain true until current is true.")]
        [SerializeField] bool persistTransition;
        bool NeverShow => false;
        enum DurationMode { before, after }

        int currentIdx;
        bool disabled;
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

        [SerializeField] float duration;

        Output currentOutput;

        [ReadOnly]
        [SerializeField] State state;


        public void AddNode()
        {
            functions.Add(new FunctionsContainer());
        }
        public void RemoveNode(int index)
        {
            functions.RemoveAt(index);
        }

        public override void Initialise(XRAvatar _avatar)
        {
            base.Initialise(_avatar);
            var args = new FunctionArgs(_avatar, _mainTracker);

            functions.ForEach(f => f.Initialise(args));
            Reset();
        }
        public void SetGestures(List<FunctionsContainer> container)
        {
            functions = container;
        }

        public override void Reset()
        {

            currentIdx = 0;
            stopWatch.Reset();
            currentOutput = Output.None;
            disabled = true;
        }

        bool PrevStillTrue() => currentIdx == 0 ?
            false :
                functions[currentIdx - 1].Run().state != State.Stopped;

        void StartTime()
        {
            stopWatch.Restart();
            currentOutput.StartSession(_mainTracker);
        }
        bool TimeExpired()
        {
            bool cond = mode == DurationMode.before ?
               stopWatch.Elapsed.TotalSeconds > duration : stopWatch.Elapsed.TotalSeconds < duration;
            if (cond)
            {
                Debug.Log("Expired");
            }

            return cond;
        }

        [Button]
        public void RunDebug()
        {
            functions.ForEach(f => f.Debug = true);
        }


        public override State Run()
        {
            //Prevent executing again when gesture has already performed.
            //Sometimes when gesture cancels/finishes, it can still be in perform state.
            //i.e toe hold.
            /*        if (disabled)
                    {
                        Debug.Log(currentIdx);
                        if (functions[0].Run(Output.None).state == State.Stopped)
                            disabled = false;
                        return Output.None;
                    }*/

            //Run the current function
            currentOutput = functions[currentIdx].Run();
            if (currentOutput.state == State.Performing)
            {
                //if first function, start timer.
                if (currentIdx == 0)
                {
                    StartTime();
                }
                //If function passes, go to next function
                currentIdx++;
                Debug.Log(currentIdx);
                if (currentIdx >= functions.Count)
                {
                    Reset();

                    state = State.Performed;
                    return state;
                }

            }//If function fails
            else if (stopWatch.IsRunning)
            {
                //Only stop if previous also fails or time is expired.
                //Since gesture can still be transitioning to next state.
                if (persistTransition && !PrevStillTrue() || TimeExpired() || currentIdx == 0)
                {
                    Reset();
                }
            }


            state = currentOutput.state;
            //If we have moved past the last function, we have performed.

            //Only output stopped
            return State.Stopped;
        }

        public void SetFunctions(List<FunctionsContainer> container)
        {
            functions = container;
        }

    }

}