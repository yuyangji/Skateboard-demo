using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    public enum UserState
    {
        Driving,
        Normal,
        HoverBoard,

    }
    public class StateManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Serializable]
        class UserStateMap
        {
            public UserState UserState;
            public List<Gesture> gestures;
        }
        [SerializeField] List<UserStateMap> m_UserStateMap;

        Dictionary<UserState, List<Gesture>> m_UserStateMapDict;

        UserState currentState;
        GestureMaster _master;
        public void SwitchState(UserState _state)
        {
            currentState = _state;
            _master.SetGestures(m_UserStateMapDict[currentState]);
        }


        public void OnAfterDeserialize()
        {
            if(m_UserStateMapDict == null)
                m_UserStateMapDict = new Dictionary<UserState, List<Gesture>>();
            m_UserStateMap.ForEach(map => m_UserStateMapDict[map.UserState] = map.gestures);
        }

        public void OnBeforeSerialize()
        {
            
        }
    }

}