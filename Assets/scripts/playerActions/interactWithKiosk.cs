using System.Collections;
using UnityEngine;

namespace SA
{
    public class interactWithKiosk : stateAction
    {
        public Camera cam;
        private float interactDistance = 11.0f;
        playerStateManager states;

        public interactWithKiosk(playerStateManager playerStates)
        {
            states = playerStates;
            cam = states.gameObject.GetComponentsInChildren<Camera>()[0];
        }

        public override void Execute()
        {
            if (states.interact)
            {
                // We create a raycast hit to try and see if we have any objects in front of us
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                // If we get a hit within distance pickUpDistance, this will return true and put our gameobject in hit
                if (Physics.Raycast(ray, out hit, interactDistance))
                {
                    // We check to see if we should pick up the hit object
                    if (hit.collider.gameObject.tag == "kiosk") {
                        states.kiosk = hit.collider.gameObject;
                        states.SetState("kioskInteract");
                    }
                }
            }
        }
    }
}