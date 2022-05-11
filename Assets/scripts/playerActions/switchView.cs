using System.Collections;
using UnityEngine;

namespace SA
{
    public class switchView : stateAction
    {
        playerStateManager states;

        public switchView (playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            
        }
    }
}