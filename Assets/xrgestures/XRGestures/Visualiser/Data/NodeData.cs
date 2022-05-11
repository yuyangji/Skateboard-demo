using System;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    [Serializable]
    public class NodeData
    {
        public string guid;
        public int progress;
        public Rect nodePosition;

        public NodeData()
        {
            guid = Guid.NewGuid().ToString();
        }
    }
}