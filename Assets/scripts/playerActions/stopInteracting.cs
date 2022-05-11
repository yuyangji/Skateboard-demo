using System.Collections;
using UnityEngine;

namespace SA
{
    public class stopInteracting : stateAction
    {
        playerStateManager states;

        public stopInteracting (playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            if (states.kiosk && states.interact) {
                states.kiosk = null;
                states.SetState("locomotion");
            }
        }
    }
}