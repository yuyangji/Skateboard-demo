using System;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{


    [Serializable]
    public class FunctionsContainer<T> where T : IFunction
    {
        [SerializeField] public bool Debug;
        [SerializeField] List<T> functions;
        Output currentOutput;

        protected virtual List<T> Functions { get => functions; set => functions = value; }

        public void Initialise(FunctionArgs args)
        {
            Functions.ForEach(f => f.Initialise(args));
        }

        //maybe remove later.
        public void DebugRun()
        {
            Functions.ForEach(f => f.DebugRun());

        }

        public Output Run()
        {
            currentOutput = Output.None;
            foreach (var f in Functions)
            {
                currentOutput = f.Run(currentOutput);
                if (currentOutput.state == State.Stopped)
                {
                    break;
                }
            }
            return currentOutput;
        }

        public void SetFunctions(List<T> _fs)
        {
            Functions = _fs;
        }
    }


    [Serializable]
    public class FunctionsContainer : FunctionsContainer<IFunction>
    {

        [SerializeReference, SubclassSelector] List<IFunction> functions;

        protected override List<IFunction> Functions { get => functions; set => functions = value; }


    }
}
