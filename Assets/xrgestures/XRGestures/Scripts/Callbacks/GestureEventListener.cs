using System;
using UnityEngine;

/// <summary>
///     Registers a response (function) to a gestureEvent.
/// </summary>
namespace XR_Gestures
{
    [Serializable]
    public class GestureEventListener<T> : IGestureListener
    {
        //The gesture event to listen to. GestureParms inherit this class.
        [SerializeField] Gesture @event; 

        //Function that processes the callback struct.
        [SerializeField] GestureCallbackFunction<T> function;

        //The function to call when event is called.
        Action<T> response;
       
        public void OnEventRaised(GestureCallback package)
        {
            response?.Invoke(function.ProcessCallback(package));
        }

        public void OnEnable(Action<T> _response)
        {
            if (@event != null) @event.RegisterListener(this);
            response = _response;
        }

        public void OnDisable()
        {
            if (@event != null) @event.UnregisterListener(this);
            response = null;
        }

    }



}
