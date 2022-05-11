using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class FeetCalibration : MonoBehaviour
{
    [SerializeField] Transform leftFoot;
    [SerializeField] Transform rightFoot;


    [SerializeField] Transform leftFootFollow;
    [SerializeField] Transform rightFootFollow;

    Quaternion offsetRotL;
    Quaternion offsetRotR;

    float offsetPosL;
    float offsetPosR;
    private void Awake()
    {
        offsetRotL = Quaternion.identity;
        offsetRotR = Quaternion.identity;
    }

    [Button]
    public void Calibrate()
    {
        offsetRotL = Quaternion.FromToRotation(leftFoot.forward, Vector3.up);
        offsetRotR = Quaternion.FromToRotation(rightFoot.forward, Vector3.up);

        offsetPosL = leftFoot.position.y;
        offsetPosR = rightFoot.position.y;

    }

    public void Update()
    {
        //Add difference to rotation.
        leftFoot.localRotation = offsetRotL * leftFootFollow.localRotation;
        leftFoot.position = leftFootFollow.position.SubY(offsetPosL);
    

        rightFoot.localRotation = offsetRotR * rightFootFollow.localRotation;
        rightFoot.position = rightFootFollow.position.SubY(offsetPosR);
    }
}
