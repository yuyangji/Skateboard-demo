using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    /// <summary>
    ///     Store a function as a scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "FunctionSO", menuName = "ScriptableObject/FunctionSO")]
    [Serializable]
    public class FunctionSO : ScriptableObject, IFunction
    {
        [SerializeReference, SubclassSelector] FunctionObject funcObj;
        public void OnDebug() => funcObj.OnDebug();

        public void Initialise(FunctionArgs _args) => funcObj.Initialise(_args);
        public Output Run(Output _res) => funcObj.Run(_res);
    }

}