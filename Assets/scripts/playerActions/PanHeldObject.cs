using System.Collections;
using UnityEngine;

namespace SA
{
    public class PanHeldObject : stateAction
    {
        private float limits = 0.4f;
        private float currentTranslationUpDown = 0.0f;
        private float currentTranslationLeftRight = 0.0f;
        private float panSpeed = 0.3f;
        private float maxPanSpeed = 3f;
        playerStateManager states;

        public PanHeldObject(playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            if (currentTranslationLeftRight < limits && states.rotateHorizontalAxis > 0)
            {
                states.currentHoldPosition = states.currentHoldPosition + new Vector3(panSpeed * states.rotateHorizontalAxis, 0.0f, 0.0f);
                currentTranslationLeftRight += panSpeed * states.rotateHorizontalAxis;
            }
            if (currentTranslationLeftRight > -limits && states.rotateHorizontalAxis < 0)
            {
                states.currentHoldPosition = states.currentHoldPosition + new Vector3(panSpeed * states.rotateHorizontalAxis, 0.0f, 0.0f);
                currentTranslationLeftRight += panSpeed * states.rotateHorizontalAxis;
            }
            if (currentTranslationUpDown < limits && states.rotateAxis > 0)
            {
                states.currentHoldPosition = states.currentHoldPosition + new Vector3(0.0f, panSpeed * states.rotateAxis, 0.0f);
                currentTranslationUpDown += panSpeed * states.rotateAxis;
            }
            if (currentTranslationUpDown > 0.1f && states.rotateAxis < 0) // This just needs to be set to 0 otherwise we blast off to space
            {
                states.currentHoldPosition = states.currentHoldPosition + new Vector3(0.0f, panSpeed * states.rotateAxis, 0.0f);
                currentTranslationUpDown += panSpeed * states.rotateAxis;
            }

            states.rb.velocity = Vector3.ClampMagnitude(states.rb.velocity, maxPanSpeed); // Clamp our max velocity to a limit without affecting the direction
        }
    }
}