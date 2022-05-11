using System.Collections;
using UnityEngine;

namespace SA
{
    public class Jump : stateAction
    {
        playerStateManager states;

        public Jump(playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            if (states.jump && states.isGrounded)
            {
                states.rb.AddForce(0, states.jumpHeight, 0, ForceMode.Impulse);
            }
        }
    }
}