using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using System.Linq;
namespace XR_Gestures
{
    public class AnimationController : MonoBehaviour
    {


        Animator animAnimator;
        AnimatorController controller;


        public List<string> GetStates()
        {
            if (animAnimator == null)
                animAnimator = GetComponent<Animator>();
            if (controller == null && animAnimator.runtimeAnimatorController)
                controller = animAnimator.runtimeAnimatorController as AnimatorController;
            if(controller)
                return controller.layers[0].stateMachine.states.ToList().Select(s => s.state.name).ToList();
            else
                return new List<string> ();
        }
            

        public void PlayAnimation(string name) 
            => animAnimator.Play(name);
    }

}