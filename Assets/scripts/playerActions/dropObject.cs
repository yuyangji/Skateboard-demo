using System.Collections;
using UnityEngine;

namespace SA
{
    public class dropObject : stateAction
    {
        public Camera cam;
        private float throwForce = 6.0f;
        playerStateManager states;

        public dropObject(playerStateManager playerStates)
        {
            states = playerStates;
            cam = states.gameObject.GetComponentsInChildren<Camera>()[0];
        }

        public override void Execute()
        {
            if (states.heldObject && states.interact) {
                states.heldObject.isKinematic = false;
                states.heldObject.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                states.heldObject = null;
                states.SetState("locomotion");
            }
        }
    }
}