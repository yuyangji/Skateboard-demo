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
        Debugger debugger;
        protected virtual List<T> Functions { get => functions; set => functions = value; }

        public void Initialise(Dictionary<string, object> _data)
        {
            Functions.ForEach(f => f.Initialise(_data));
            debugger = (Debugger) _data[DataKeyConstants.Debugger];
        }

        //maybe remove later.
        public void DebugRun()
        {
            Functions.ForEach(f =>
            {
                debugger.AddTitle(f.GetType().Name);
                f.DebugRun();

            });

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

        public FunctionsContainer()
        {
            functions = new List<IFunction>();
            functions.Add(new FeetPose());
        }

        protected override List<IFunction> Functions { get => functions; set => functions = value; }


    }
}
