using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XR_Gestures;
public class LocomotionController : MonoBehaviour
{
    [SerializeField] GestureCallbackManager link;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ManageMovement();
        CheckJump();
    }

    void ManageMovement()
    {
        rb.velocity = link.move;
    }
    void CheckJump()
    {
        if (link.jump)
        {

        }
        link.jump = false;
    }


}
