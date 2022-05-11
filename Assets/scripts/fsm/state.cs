using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class state
    {
        public List<stateAction> actions = new List<stateAction>();
        public List<stateAction> fixedActions = new List<stateAction>(); //These are actions that happen in fixed ticks. Basically, things which need physics

        public void Tick()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Execute();
            }
        }
        
        public void FixedTick()
        {
            for (int i = 0; i < fixedActions.Count; i++)
            {
                fixedActions[i].Execute();
            }
        }
    }
}
