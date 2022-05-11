using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class stateManager : MonoBehaviour
    {
        // This is our state manager. It manages our different states so that they may have an effect on the game world.

        public float fixedDelta; // We use this for frame based calculations, basically for things involving physics
        public float delta;
        public Transform myTransform; // This is the object we're managing the state of. By default it gets set on start.

        protected Dictionary<string, state> allStates = new Dictionary<string, state>(); // Our dictionary of valid states
        state currentState;

        state GetState(string targetState)
        {
            state retVal = null;
            allStates.TryGetValue(targetState, out retVal); // If the state exists we get it, otherwise it returns null, pretty nifty
            return retVal;
        }

        public void SetState(string targetState)
        {
            currentState = GetState(targetState);
        }

        private void Start()
        {
            myTransform = this.transform;
            Init();
        }

        protected abstract void Init();

        public void FixedTick()
        {
            // This is used for all fixed tick actions. Basically, we get our state to execute all of our state actions for fixed time actions
            fixedDelta = Time.fixedDeltaTime;
            if (currentState != null)
            {
                currentState.FixedTick();
            }
        }

        private void Update()
        {
            // This gets our state to execute all applicable actions.
            delta = Time.deltaTime;

            if (currentState != null)
            {
                currentState.Tick();
            }
        }
    }
}