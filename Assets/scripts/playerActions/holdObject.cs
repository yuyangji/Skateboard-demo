using System.Collections;
using UnityEngine;

namespace SA
{
    public class holdObject : stateAction
    {
        private float lerpSpeed = 25.0f;
        playerStateManager states;

        public holdObject(playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            if (states.heldObject) { states.heldObject.MovePosition(Vector3.Lerp(states.heldObject.position, states.currentHoldPosition, Time.deltaTime * lerpSpeed)); }
        }
    }
}