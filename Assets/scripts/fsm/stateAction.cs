using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    // This is just an abstract for any action. All it does by default is execute, and is used by state for making things happen
    public abstract class stateAction
    {
        public abstract void Execute();
    }
}
