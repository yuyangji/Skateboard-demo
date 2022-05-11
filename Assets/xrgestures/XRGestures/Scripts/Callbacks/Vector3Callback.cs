using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XR_Gestures
{
    [CreateAssetMenu(menuName = "ScriptableObject/Vector3Callback", fileName = "new Vector3Callback")]
    public class Vector3Callback : GestureCallbackFunction<Vector3>
    {
        [SerializeField] float  maxX;
        [SerializeField] float  maxY;

        [SerializeField] bool useXDelta;
        [SerializeField] bool useYDelta;

        [SerializeField] bool useToe;
        [SerializeField] bool useHeel;
        public override Vector3 ProcessCallback(GestureCallback g)
        {
            return useYDelta ? UseYDelta(g) : UseXDelta(g);
        }

        private Vector3 UseXDelta(GestureCallback g)
        {
            float normalX = Mathf.Clamp(g.Position.x / maxX, 0f, 1f);
            return new Vector3(normalX, 0f, 0f);
        }
        private Vector3 UseYDelta(GestureCallback g)
        {
            float normalY = Mathf.Clamp( g.Position.y / maxY,  0f, 1f);
            return new Vector3(0f, normalY, 0f);
          
        }

    }
}

