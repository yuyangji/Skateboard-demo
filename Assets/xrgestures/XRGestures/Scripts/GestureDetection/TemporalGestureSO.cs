using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;


namespace XR_Gestures
{


    [CreateAssetMenu(fileName = "NewGesture", menuName = "ScriptableObject/Gesture (Time based)")]
    public class TemporalGestureSO : Gesture
    {
        bool NeverShow => false;
        [ShowIf("NeverShow")]
        [SerializeField] List<FunctionsContainer> functions = new List<FunctionsContainer>();
        [SerializeField] DurationMode mode;

        [InfoBox("Previous function must remain true until current is true.")]
        [SerializeField] bool persistTransition;

        enum DurationMode { before, after }

        int currentIdx;

        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

        [SerializeField] float duration;

        Output currentOutput;

        private void Awake()
        {
            functions.Add(new FunctionsContainer());
        }
        private void OnValidate()
        {
            if (functions.Count == 0)
            {
                functions.Add(new FunctionsContainer());
            }
        }

        public override void Initialise(XRAvatar _avatar)
        {
            base.Initialise(_avatar);

            functions.ForEach(f => f.Initialise(data));
            ResetFunction();
        }
        public void SetGestures(List<FunctionsContainer> container)
        {
            functions = container;
        }

        public override void ResetFunction()
        {
            currentIdx = 0;
            stopWatch.Reset();
            currentOutput = Output.None;
        }

        bool PrevStillTrue() => currentIdx == 0 ?
            false :
                functions[currentIdx - 1].Run().state != State.Stopped;

        void StartTime()
        {
            stopWatch.Restart();

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



        public override State Run()
        {
            if (RunDebug)
            {
                debugger.ClearText();
                functions.ForEach(f => f.DebugRun());

            }

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
                if (currentIdx >= functions.Count)
                {
                    ResetFunction();

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
                    ResetFunction();
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