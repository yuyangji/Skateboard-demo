using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XR_Gestures
{
    public class GestureOutputManager : MonoBehaviour, IGestureListener
    {
        [Serializable]
        class GestureKey
        {
            [SerializeField] public Gesture gesture;
            [SerializeField] public string key;
        }

        [SerializeField] List<GestureKey> gestures;


        Dictionary<string, Gesture> gestureKeyDict = new Dictionary<string, Gesture>();

        Dictionary<Gesture, bool> inputDict = new Dictionary<Gesture, bool>();

        private void Awake()
        {
            gestures.ForEach(g =>
            {
                g.gesture.RegisterListener(this);
                inputDict[g.gesture] = false;
                gestureKeyDict[g.key] = g.gesture;
            });

        }

        
        public void OnEventRaised(GestureCallback _callback)
        {
            inputDict[_callback.gesture] = true;
         
        }

      

        public bool GetInput(string _key)
        {
            return inputDict[gestureKeyDict[_key]];
        }

        private void LateUpdate()
        {

            foreach (Gesture key in inputDict.Keys.ToList())
            {
                inputDict[key] = false;
               
            }

        }
    }



}