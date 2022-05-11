using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class maps gestures to a specific action. 
/// 
/// </summary>

namespace XR_Gestures
{

    public class GestureCallbackManager : MonoBehaviour
    {
        
        [SerializeField] GestureEventListener<bool> jumpEvent;
        [SerializeField] GestureEventListener<Vector3> moveEvent;

        [SerializeField] GestureEventListener<Vector3> swipeEvent;
        public Vector3 move;
        public bool jump;

        private void Awake()
        {
            jumpEvent.OnEnable(Jump);
            moveEvent.OnEnable(MoveInput);


        }

        public void MoveInput(Vector3 _direction)
        {
            move = _direction;
     
        }
        public void Jump(bool _state)
        {
            jump = _state; 
        }

        public void MoveSkateboard(Vector3 _dir)
        {
            //move skateboard. 
        }

        public void ApplyForce(float force, Vector3 dir)
        {

        }

    }
}