using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
namespace XR_Gestures
{
    public class GestureTester : MonoBehaviour
    {
        [SerializeField] XRAvatar avatar;
        [BoxGroup("")]

        [Expandable]
        [SerializeField] Gesture gesture;


        [BoxGroup("Vars")]
        [ReadOnly]
        [SerializeField]
        State state;
        private class TrueFunction : IFunction
        {
            public void Initialise(FunctionArgs _args){}

            public void OnDebug(){}

            public Output Run(Output _res)
            {
                return _res.ChangeState(State.Performing);
            }
        }

        private class FalseFunction : IFunction
        {
            public void Initialise(FunctionArgs _args)
            {
              
            }

            public void OnDebug()
            {
         
            }

            public Output Run(Output _res)
            {
                return _res.ChangeState(State.Stopped);
            }
        }
        private void Start()
        {

            gesture.Initialise(avatar);
           /* TemporalGestureSO temporalGestureSO = gesture as TemporalGestureSO;
            FunctionsContainer container = new FunctionsContainer();
            FunctionsContainer container2 = new FunctionsContainer();
            container.SetFunctions(new List<IFunction>() { new TrueFunction() });
            container2.SetFunctions(new List<IFunction>() { new FalseFunction()});
            temporalGestureSO.SetGestures(new List<FunctionsContainer>() { container, container2 });*/

        }


        private void Update()
        {
            if (gesture == null) return;
            state = gesture.Run();
            if(state == State.Performed)
            {
                Debug.Log(state);
            }
/*
            if (state == State.Stopped)
            {
                Debug.Log(state);
            }*/

        }
    }

}