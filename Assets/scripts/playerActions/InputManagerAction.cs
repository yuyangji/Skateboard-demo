using System.Collections;
using UnityEngine;
using XR_Gestures;
namespace SA
{
    public class InputManagerAction : stateAction
    {
        playerStateManager states;
        GestureOutputManager manager;
        public InputManagerAction( playerStateManager playerStates)
        {
            states = playerStates;
            manager = playerStates.outputManager;
        }

        public override void Execute()
        {
            /*states.horizontal = Input.GetAxis("Horizontal");
            states.vertical = Input.GetAxis("Vertical");
            states.rotateAxis = Input.GetAxis("Rotate");
            states.rotateHorizontalAxis = Input.GetAxis("RotateHorizontal");
            states.jump = Input.GetKey  (KeyCode.Space);
            states.interact = Input.GetKeyDown(KeyCode.E);
            states.cameraInputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));*/

            states.horizontal = axisCalculate(manager.GetInput("horizontalPos"), manager.GetInput("horizontalNeg"), 0.2f, states.horizontal);
            states.vertical = axisCalculate(manager.GetInput("verticalPos"), manager.GetInput("verticalNeg"), 0.2f, states.vertical);

            states.jump = manager.GetInput("jump");

            states.rotateAxis = Input.GetAxis("Rotate");
            states.rotateHorizontalAxis = Input.GetAxis("RotateHorizontal");
            
            states.interact = Input.GetKeyDown(KeyCode.E);
            states.cameraInputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }

        private float axisCalculate(bool gesturePositive, bool gestureNegative, float sensitivity, float axis)
        {
            if (gesturePositive)
            {
                axis = Mathf.Lerp(axis, 1, sensitivity);
            }

            else if (gestureNegative)
            {
                axis = Mathf.Lerp(axis, -1, sensitivity);
            }
            else
            {
                axis = Mathf.Lerp(axis, 0, sensitivity);
            }


            return axis;
        }

        // axis(gesturePositive, gestureNegative, sensitivity)
    }
}