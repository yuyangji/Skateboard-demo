using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using NaughtyAttributes;
namespace XR_Gestures
{
    [CreateAssetMenu(fileName = "NewGesture", menuName = "ScriptableObject/Gesture (Repeating)")]
    public class RepeatingGesture : Gesture
    {
        [Expandable]
        [SerializeField] List<Gesture> gestures;

        Stopwatch timer = new Stopwatch();
        int currentIdx;

        [SerializeField] float maxTime;

        State currentState;
        public override void Initialise(XRAvatar _avatar)
        {
            base.Initialise(_avatar);
            gestures.ForEach(gesture => gesture.Initialise(_avatar));
        }
        public override void Reset()
        {
            currentIdx = 0;
            timer.Reset();
            gestures.ForEach(_gesture => _gesture.Reset());
        }

        bool TimeExpired()
        {
            return timer.Elapsed.TotalSeconds > maxTime;
        }

        public override State Run()
        {
           State? result = null;
           for(int i = 0; i < gestures.Count; i++)
           {
                if(currentIdx == i)
                {
                    result = gestures[i].Run();

                }
                else
                {
                    gestures[i].Run();
                }           
           }
           if(result == State.Performed)
            {
                if(currentIdx == gestures.Count - 1)
                {
                    Reset();
                    timer.Start();
                }
                else
                {
                    currentIdx++;
                }

            }
      

           if(timer.IsRunning)
            {
                if (TimeExpired())
                {
                    Reset();
                    return State.Stopped;
                }
                return State.Performing;
            }
            else
            {
                return State.Stopped;
            }


        }

    }

}