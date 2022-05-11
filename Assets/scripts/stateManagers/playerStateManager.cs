using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{

    public class playerStateManager : stateManager
    {
        // This is our player state manager. It controls our player and what they can do. It allows us to set our inputs and so on.


        [Header("Inputs")]
        // movement inputs
        public float horizontal = 0.0f;
        public float vertical = 0.0f;
        public float rotateAxis = 0.0f;
        public float rotateHorizontalAxis = 0.0f;
        public bool jump;
        public bool interact; //Our interaction key, used for picking up objects and interacting with doors and kiosks
        public Vector2 cameraInputValues;

        [Header("Grinding")]
        public bool grinding = false;
        public Vector3 grindVector;

        [Header("Jumping")]
        [SerializeField] public float jumpHeight = 7;
        public float jumpTolerance = 1.6f;
        public bool isGrounded = false;
        [SerializeField] public float cooldown = 0.5f;
        public float skateCooldown = 0f;

        [Header("Movement")]
        [SerializeField] public float vAxisMovementSpeed = 3f;
        [SerializeField] public float maxSpeed = 13f;        

        [Header("References")]
        public Rigidbody rb; // This is us
        public GameObject skateBoardBody; // This is the render of our skateboard

        public Rigidbody heldObject; // This is what we are holding in our examine mode
        public Transform objectHolder; // This is where we are holding it to
        public Vector3 currentHoldPosition; // This is used to pan the object around

        public GameObject kiosk; // This is the kiosk we're interacting with

        [SerializeField] public XR_Gestures.GestureOutputManager outputManager;
        protected override void Init()
        {
            #region Locomotion State
            state locomotion = new state();
            // These our our fixed updates. They occur before normal updates so we put them first.
            locomotion.fixedActions.Add(new MoveObjectWithPhysics(this)); // our forward and backwards movement
            locomotion.fixedActions.Add(new Rotate(this)); // our board rotation
            locomotion.fixedActions.Add(new checkGrounded(this));

            locomotion.actions.Add(new hoverBoardBob(this)); //This makes our board tilt with our movement
            locomotion.actions.Add(new controlCamera(this));
            locomotion.actions.Add(new Jump(this)); //This is our "hover"
            locomotion.actions.Add(new pickUpObject(this));
            locomotion.actions.Add(new interactWithKiosk(this));
            //locomotion.actions.Add(new interactWithDoor(this));
            //locomotion.actions.Add(new interactWithUI(this)); //This lets us fiddle with our UI
            locomotion.actions.Add(new InputManagerAction(this));
            #endregion

            #region Examine Object State
            state examine = new state();
            examine.actions.Add(new holdObject(this));

            examine.fixedActions.Add(new RotateHeldObject(this));
            examine.fixedActions.Add(new PanHeldObject(this));
            examine.fixedActions.Add(new dropObject(this));

            examine.actions.Add(new InputManagerAction(this));
            #endregion

            #region Interact with Kiosk state
            state kioskInteract = new state();
            kioskInteract.fixedActions.Add(new switchView(this));
            kioskInteract.fixedActions.Add(new stopInteracting(this));

            kioskInteract.actions.Add(new InputManagerAction(this));
            #endregion

            //#region Toggle UI State
            state UIstate = new state();

            //UIstate.actions.Add(new changeView(this)); //Changes our UI view
            //UIstate.actions.Add(new changeHue(this)); //changes the UI's colour
            //UIstate.actions.Add(new stopUIState(this)); //Goes back to the locomotion state

            //UIstate.actions.Add(new InputManagerAction(this));
            //#endregion

            allStates.Add("locomotion", locomotion);
            allStates.Add("examine", examine);
            allStates.Add("kioskInteract", kioskInteract);
            //allStates.Add("UIstate", UIstate);

            SetState("locomotion");
        }

        private void FixedUpdate()
        {
            FixedTick();
        }
    }
}
