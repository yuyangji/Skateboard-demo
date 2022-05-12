using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    public interface IFunction
    {
        public void Initialise(FunctionArgs _args);
        public Output Run(Output _res);

        public void DebugRun();
    }
}