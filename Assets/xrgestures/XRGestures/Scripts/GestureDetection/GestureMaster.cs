using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace XR_Gestures
{

    public class GestureMaster : MonoBehaviour
    {
        // --- Editor event 
        public UnityEvent Validated;


        [SerializeField] XRAvatar avatar;
        [Expandable]
        [SerializeField] List<Gesture> gestures;

        public event EventHandler<Gesture> OnGestureSelected;

        [SerializeField]
        [ReadOnly]
        List<Gesture> PossibleGestures = new List<Gesture>();

        [SerializeField]
        [ReadOnly]
        Gesture currentGesture;
        Gesture prevGesture;

        //Handle simulatenous Gestures
        Dictionary<Gesture, List<Gesture>> concurrentGesturesDict;
        List<Gesture> concurrentGestures;
        private void Awake()
        {
            concurrentGesturesDict = new Dictionary<Gesture, List<Gesture>>();
            gestures.ForEach(g =>
            {
                g.Initialise(avatar);
                BuildConcurrent(g);
            });

        }

        void BuildConcurrent(Gesture _gesture)
        {
            if (_gesture.concurrentGestures != null)
            {
                concurrentGesturesDict[_gesture] = _gesture.concurrentGestures.FindAll(g => gestures.Contains(g));
            }
        }

        public void SetGestures(List<Gesture> _gestures)
        {

            gestures = _gestures;
            gestures.ForEach(g => g.Initialise(avatar));
        }

        private void Update()
        {

            ProcessGestures();
        }

        //Process gestures.
        void ProcessGestures()
        {
            if (currentGesture == null)
            {
                FindGesture();
            }

            if (currentGesture != null)
            {
                if (concurrentGestures != null)
                {
                    concurrentGestures.ForEach(g =>
                    {
                        State res = g.Run();
                        if (res == State.Performed)
                        {
                            PerformGesture(g);
                        }
                    });
                }

                GestureUpdate();
            }
        }


        //Finds gestures. If there is only one possible, then select it.
        //If there is more than one, filter until there is only one.
        void FindGesture()
        {

            PossibleGestures.Clear();
            foreach (Gesture g in gestures)
            {
                State res = g.Run();
                if (res == State.Performed)
                {
                    PerformGesture(g);
                    break;
                }
                else if (res == State.Performing)
                {
                    PossibleGestures.Add(g);
                }
            };
            if (PossibleGestures.Count == 1)
            {
                SelectGesture(PossibleGestures[0]);
            }
        }

        void PerformGesture(Gesture _gesture)
        {
            Debug.Log("Executed " + _gesture.ToString());
            _gesture.RaiseEvent();
        }

        //Select the gesture, and clear possible gestures
        void SelectGesture(Gesture _gesture)
        {  //If previous gesture is still in active mode, wait till it isn't
            /*            if (prevGesture != null && prevGesture == _gesture)
                            return;
                        else prevGesture = null;
            */

            currentGesture = _gesture;
            OnGestureSelected?.Invoke(this, currentGesture);
            currentGesture.RaiseEvent();
            concurrentGesturesDict.TryGetValue(_gesture, out concurrentGestures);
        }


        //Stops the gesture
        void StopGesture()
        {
            prevGesture = currentGesture;
            currentGesture = null;
            concurrentGestures = null;
        }


        //Update the gesture
        void GestureUpdate()
        {
            State res = currentGesture.Run();

            //Event callback
            currentGesture.RaiseEvent();

            //If performed, then gesture is done. 
            if (res == State.Performed)
            {
                Debug.Log("Executed " + currentGesture.ToString());
                StopGesture();
            }
            //If cancelled, or no longer activated, then gesture is done.
            else if (res == State.Stopped)
            {

                Debug.Log("Stopped " + currentGesture.ToString());
                StopGesture();


                prevGesture = null;
            }

        }

    }

    public struct GestureCallback
    {
        public Vector2 Position;
        public Vector3 Velocity;
        public State res;
        public Gesture gesture;
    }


}
