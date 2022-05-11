using System.Collections;
using UnityEngine;

namespace SA
{
    public class controlCamera : stateAction
    {
        playerStateManager states;

        [SerializeField] private float lookSensitiivty;
        [SerializeField] private float smoothing;
        [SerializeField] private int maxLookRotation = 80;

        private Vector2 smoothedVelocity;
        private Vector2 currentLookingPosition;


        public controlCamera(playerStateManager playerStates)
        {
            states = playerStates;
        }

        public override void Execute()
        {
            Vector2 smoothInputValues = Vector2.Scale(states.cameraInputValues, new Vector2(lookSensitiivty * smoothing, lookSensitiivty * smoothing)); // We scale our input values by our looksensitivity times our smoothing

            smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, smoothInputValues.x, 1f / smoothing); // Linearly interpolate how much our camera is going to pivot by a smoothing value. Makes it less jumpy when you move around
            smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, smoothInputValues.y, 1f / smoothing);

            currentLookingPosition += smoothedVelocity;
            currentLookingPosition.y = Mathf.Clamp(currentLookingPosition.y, -maxLookRotation, maxLookRotation);

            // your character is very drunk mode:
            //transform.Rotate(-currentLookingPosition.y * 0.003f, currentLookingPosition.x * 0.003f, 0);
            // normal mode
            //states.transform.localRotation = Quaternion.AngleAxis(-currentLookingPosition.y, Vector3.right) * Quaternion.AngleAxis(currentLookingPosition.x, states.transform.up);
            
            
            //This let's the player look up and down
            //player.transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up);
            //transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up);
        }
    }
}