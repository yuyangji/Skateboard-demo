using System.Collections;
using UnityEngine;

namespace SA
{
    public class hoverBoardBob : stateAction
    {
        private float tiltSpeed = 0.03f; // Our speed of tilting
        playerStateManager states;
        // Our 3 Quaternions for our default look rotation, our forward rotation and our back rotation
        Quaternion defaultRotation;
        Quaternion forwardRotation;
        Quaternion backRotation;


        public hoverBoardBob(playerStateManager playerStates)
        {
            states = playerStates;
            defaultRotation = states.skateBoardBody.transform.rotation;
            forwardRotation = Quaternion.Euler(new Vector3(12, defaultRotation.y, defaultRotation.z));
            backRotation = Quaternion.Euler(new Vector3(-12, defaultRotation.y, defaultRotation.z));
        }

        public override void Execute()
        {
            float rotationTemp = Quaternion.Lerp(states.skateBoardBody.transform.rotation, defaultRotation, tiltSpeed).eulerAngles.x;

            if (states.vertical > 0)
            {
                rotationTemp = Quaternion.Lerp(states.skateBoardBody.transform.rotation, forwardRotation, tiltSpeed).eulerAngles.x;
            }
            else if (states.vertical < 0)
            {
                rotationTemp = Quaternion.Lerp(states.skateBoardBody.transform.rotation, backRotation, tiltSpeed).eulerAngles.x;
            }

            Vector3 curRotation = states.skateBoardBody.transform.rotation.eulerAngles;
            states.skateBoardBody.transform.rotation = Quaternion.Euler(new Vector3(rotationTemp, curRotation.y, curRotation.z));

            states.skateBoardBody.transform.position = states.rb.transform.position; // We need to lock our position like this because we're getting a rotation
            // stretching bug if we just parent it, which is annoying.

            if (states.skateBoardBody.transform.rotation.eulerAngles.x > 30) // Prevent it from breaking, which also happens sometimes for some reason
            {
                states.skateBoardBody.transform.rotation = Quaternion.Euler(new Vector3(12, curRotation.y, curRotation.z));
            }
            if (states.skateBoardBody.transform.rotation.eulerAngles.x < -30) 
            {
                states.skateBoardBody.transform.rotation = Quaternion.Euler(new Vector3(-12, curRotation.y, curRotation.z));
            }


        }
    }
}