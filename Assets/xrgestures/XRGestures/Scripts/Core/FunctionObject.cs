using System;
using System.Collections.Generic;
using UnityEngine;
namespace XR_Gestures
{


    [Serializable]
    public abstract class FunctionObject : IFunction
    {
        [HideInInspector] public string name;

        //NOT condition.
        [SerializeField] bool negate;

        protected Tracker mainTracker;
        protected XRAvatar avatar;
        protected Debugger debugger;
        public void OnAfterDeserialize()
            => name = this.GetType().Name;

        public void OnBeforeSerialize()
            => name = this.GetType().Name;


        public virtual void DebugRun()
        {
            if (debugger == null)
            {
                return;
            }
        }

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

        public virtual void Initialise(Dictionary<string, object> _data)
        {
            mainTracker = (Tracker) _data[DataKeyConstants.MainTracker];
            avatar = (XRAvatar) _data[DataKeyConstants.Avatar];
            debugger = (Debugger) _data[DataKeyConstants.Debugger];
        }
    }

}
