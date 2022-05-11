using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    public class FloatCallback : GestureCallbackFunction<float>
    {
        // Start is called before the first frame update


        public override float ProcessCallback(GestureCallback g)
        {
            return 0.0f;
        }


    }

}
