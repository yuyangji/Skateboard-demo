using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XR_Gestures
{
    [CreateAssetMenu(menuName = "ScriptableObject/BooleanCallback", fileName = "gesture bool callback")]
    public class BooleanCallback : GestureCallbackFunction<bool>
    {
        [SerializeField] State trueState;

        public override bool ProcessCallback(GestureCallback g)
            => g.res == trueState;

    }

}


