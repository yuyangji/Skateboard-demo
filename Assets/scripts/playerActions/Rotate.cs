using System.Collections;
using UnityEngine;

namespace SA
{
    public class Rotate : stateAction
    {
        playerStateManager states;

        public Rotate(playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            states.rb.transform.Rotate(0, states.horizontal * 2, 0);
            states.skateBoardBody.transform.Rotate(0, states.horizontal * 2, 0);
        }
    }
}