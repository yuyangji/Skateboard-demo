using System.Collections;
using UnityEngine;

namespace SA
{
    public class RotateHeldObject : stateAction
    {
        playerStateManager states;
        private float rotateRate = 2.0f;

        public RotateHeldObject(playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            states.heldObject.transform.Rotate(states.vertical * rotateRate, states.horizontal * rotateRate, 0);
        }
    }
}