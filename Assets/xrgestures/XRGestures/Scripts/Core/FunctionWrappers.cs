using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace XR_Gestures
{
    //Allows to input a function saved as scriptable object.
    [AddTypeMenu("ScriptableObject/FunctionWrapper")]
    [Serializable]
    public class FunctionSOWrapper : IFunction
    {
        [Expandable]
        [SerializeField] FunctionSO functionSO;

        public void OnDebug() => functionSO.OnDebug();

        public void Initialise(FunctionArgs _args) => functionSO.Initialise(_args);
        public Output Run(Output _res) => functionSO.Run(_res);

    }

    [AddTypeMenu("ScriptableObject/PoseWrapper")]
    [Serializable]
    public class FunctionsContainerWrapper : IFunction
    {
        [Expandable]
        [SerializeField] PoseSO functionsSO;

        public void OnDebug() => functionsSO.OnDebug();

        public void Initialise(FunctionArgs _args) => functionsSO.Initialise(_args);
        public Output Run(Output _res) => functionsSO.Run(_res);

        public void SetFunctions(List<AlignedInDirection> _fs)
        {
            functionsSO.SetFunctions(_fs);
        }

    }







}
