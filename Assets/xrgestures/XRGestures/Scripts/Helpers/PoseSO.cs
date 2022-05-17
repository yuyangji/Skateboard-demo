using System;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    [Serializable]
    public class PoseSO : ScriptableObject, IFunction
    {
        [SerializeField] FunctionsContainer<AlignedInDirection> functionsSO = new FunctionsContainer<AlignedInDirection>();

        public void DebugRun()
        {
            functionsSO.DebugRun();
        }

        public void Initialise(Dictionary<string, object> _data) => functionsSO.Initialise(_data);
        public Output Run(Output _res) => functionsSO.Run();

        public void SetFunctions(List<AlignedInDirection> _fs)
        {
            functionsSO.SetFunctions(_fs);
        }

    }
}