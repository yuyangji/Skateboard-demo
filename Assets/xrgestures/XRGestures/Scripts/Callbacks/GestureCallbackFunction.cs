
using UnityEngine;

namespace XR_Gestures
{
    //Processes callback struct to output desired type.
    public abstract class GestureCallbackFunction<T> : ScriptableObject
    {
        public abstract T ProcessCallback(GestureCallback g);

    }
}