using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    [Serializable]
    public class PoseSO : ScriptableObject, IFunction
    {
        [SerializeField] FunctionsContainer<AlignedInDirection> functionsSO = new FunctionsContainer<AlignedInDirection>();

        public void OnDebug()
        {
           
        }

        public void Initialise(FunctionArgs _args) => functionsSO.Initialise(_args);
        public Output Run(Output _res) => functionsSO.Run();

        public void SetFunctions(List<AlignedInDirection> _fs)
        {
            functionsSO.SetFunctions(_fs);
        }

    }
}