using System.Collections;
using UnityEngine;

namespace SA
{
    public class MoveObjectWithPhysics : stateAction
    {
        playerStateManager states;

        public MoveObjectWithPhysics(playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            states.rb.AddRelativeForce(0, 0, states.vAxisMovementSpeed * states.vertical, ForceMode.Impulse);
            states.rb.velocity = Vector3.ClampMagnitude(states.rb.velocity, states.maxSpeed); // Clamp our max velocity to a limit without affecting the direction
            states.skateCooldown = states.cooldown;
        }
    }
}