using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Gestures
{
    [RequireComponent(typeof(XRAvatar))]
    public class TrackerManager : MonoBehaviour
    {
        XRAvatar _avatar;
        private void Awake()
        {
            _avatar = GetComponent<XRAvatar> ();
        }

        public void SetGround()
        {

        }


    }

}