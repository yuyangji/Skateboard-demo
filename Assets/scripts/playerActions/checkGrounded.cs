using System.Collections;
using UnityEngine;

namespace SA
{
    public class checkGrounded : stateAction
    {

        playerStateManager states;

        public checkGrounded(playerStateManager playerStates)
        {
            states = playerStates;
        }

        // Use this for initialization
        public override void Execute()
        {
            states.isGrounded = (Physics.Raycast(states.rb.transform.position, Vector3.down, states.jumpTolerance));
        }
    }
}