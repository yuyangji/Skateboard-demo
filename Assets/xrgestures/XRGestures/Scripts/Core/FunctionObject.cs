using System;
using UnityEngine;
namespace XR_Gestures
{


    [Serializable]
    public abstract class FunctionObject : IFunction
    {
        [HideInInspector] public string name;

        //NOT condition.
        [SerializeField] bool negate;

        public void OnAfterDeserialize()
            => name = this.GetType().Name;

        public void OnBeforeSerialize()
            => name = this.GetType().Name;


        public virtual void DebugRun() { }

        public virtual Output Run(Output res)
        {
            if (Function())
            {
                return negate ?
                    res.ChangeState(State.Stopped) :
                    res.ChangeState(State.Performing);
            }
            else
            {
                return negate ?
                    res.ChangeState(State.Performing) :
                    res.ChangeState(State.Stopped);
            }


        }

        protected abstract bool Function();

        public abstract void Initialise(FunctionArgs _args);
    }



    public struct FunctionArgs
    {
        public XRAvatar avatar;
        public Tracker mainTracker;

        public FunctionArgs(XRAvatar _avatar, Tracker _tracker) => (avatar, mainTracker) = (_avatar, _tracker);

    }
}
