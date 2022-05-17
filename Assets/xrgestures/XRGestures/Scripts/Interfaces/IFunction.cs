using System.Collections.Generic;

namespace XR_Gestures
{
    public interface IFunction
    {
        public void Initialise(Dictionary<string, object> _args);
        public Output Run(Output _res);

        public void DebugRun();
    }
}