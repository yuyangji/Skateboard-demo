using NaughtyAttributes;
using UnityEngine;
using XR_Gestures;
public class FootSimulator : MonoBehaviour
{
    [SerializeField] XRAvatar avatar;
    Tracker toeL, heelL, toeR, heelR;

    public Transform leftFootMesh, rightFootMesh;

    float leftDist;
    float rightDist;

    public Transform heelLFake, heelRFake;


    [Header("Toe/Heel Movement")]
    [Range(-0.15f, 0.15f)]
    public float leftToeX;

    [Range(-0.15f, 0.15f)]
    public float leftHeelX;

    [Range(-0.15f, 0.15f)]
    public float rightToeX;


    [Range(-0.15f, 0.15f)]
    public float rightHeelX;

    [Header("Foot Movement")]
    [Range(-1f, 1f)]
    public float LeftMoveX;
    [Range(-1f, 1f)]
    public float LeftMoveZ;


    [Range(-1f, 1f)]
    public float RightMoveX;
    [Range(-1f, 1f)]
    public float RightMoveZ;


    [SerializeField]
    public float liftHeight;

    float LiftLeftToe;
    float LiftLeftHeel;
    float LiftRightToe;
    float LiftRightHeel;
    Vector3 toeLstartPos, heelLstartPos, toeRstartPos, heelRstartPos;
    void GetTrackers()
    {
        if (avatar == null)
        {
            return;
        }

        toeL = avatar.GetTracker(TrackerLabel.toe_L);
        heelL = avatar.GetTracker(TrackerLabel.heel_L);
        toeR = avatar.GetTracker(TrackerLabel.toe_R);
        heelR = avatar.GetTracker(TrackerLabel.heel_R);
    }

    private void OnValidate()
    {
        GetTrackers();
        if (leftFootMesh)
        {
            leftFootMesh.position = toeL.Position;
        }
        if (rightFootMesh)
        {
            rightFootMesh.position = toeR.Position;
        }

    }

    private void Awake()
    {
        GetTrackers();
        SetStartPositions();
        leftDist = Vector3.Distance(toeL.Position, heelLFake.position);
        rightDist = Vector3.Distance(toeR.Position, heelRFake.position);
    }

    void SetStartPositions()
    {
        heelLFake.position = heelL.Position;
        heelRFake.position = heelR.Position;

        heelLstartPos = heelLFake.localPosition;
        heelRstartPos = heelRFake.localPosition;

        toeLstartPos = toeL.LocalPosition;
        toeRstartPos = toeR.LocalPosition;

    }

    [Button]
    private void ResetPositions()
    {
        leftToeX = 0f;
        leftHeelX = 0f;
        rightToeX = 0f;
        rightHeelX = 0f;
        LeftMoveX = 0f;
        LeftMoveZ = 0f;
        RightMoveX = 0f;
        RightMoveZ = 0f;
        liftHeight = 0f;
        LiftLeftToe = 0f;
        LiftLeftHeel = 0f;
        LiftRightToe = 0f;
        LiftRightHeel = 0f;
    }


    [Button("Left Toe")]
    void LeftToe()
    {
        if (LiftLeftToe >= liftHeight)
        {
            LiftLeftToe = 0f;

        }
        else
        {
            LiftLeftToe = liftHeight;
        }
    }

    [Button("Left Heel")]
    void LeftHeel()
    {
        if (LiftLeftHeel >= liftHeight)
        {
            LiftLeftHeel = 0f;

        }
        else
        {
            LiftLeftHeel = liftHeight;
        }
    }
    [Button("Right Toe")]
    void RightToe()
    {
        if (LiftRightToe >= liftHeight)
        {
            LiftRightToe = 0f;

        }
        else
        {
            LiftRightToe = liftHeight;
        }
    }


    [Button("Right Heel")]
    void RightHeel()
    {
        if (LiftRightHeel >= liftHeight)
        {
            LiftRightHeel = 0f;

        }
        else
        {
            LiftRightHeel = liftHeight;
        }
    }

    //Z is up
    private void Update()
    {
        var leftZOffset = Mathf.Sqrt((leftDist * leftDist) - (leftToeX * leftToeX));
        var rightZOffset = Mathf.Sqrt((rightDist * rightDist) - (rightToeX * rightToeX));
        //zOffset *= (leftToeX == 0f ? 0 : 1);

        toeL.transform.localPosition = toeLstartPos.AddXYZ(leftToeX, LiftLeftToe, -(leftDist - leftZOffset));
        toeR.transform.localPosition = toeRstartPos.AddXYZ(rightToeX, LiftRightToe, -(rightDist - rightZOffset));


        heelLFake.localPosition = heelLstartPos.AddXY(leftHeelX, LiftLeftHeel);
        heelRFake.localPosition = heelRstartPos.AddXY(rightHeelX, LiftRightHeel);


        toeL.transform.localPosition = toeL.transform.localPosition.AddXZ(LeftMoveX, LeftMoveZ);
        toeR.transform.localPosition = toeR.transform.localPosition.AddXZ(RightMoveX, RightMoveZ);


        heelLFake.localPosition = heelLFake.localPosition.AddXZ(LeftMoveX, LeftMoveZ);
        heelRFake.localPosition = heelRFake.localPosition.AddXZ(RightMoveX, RightMoveZ);

        var leftDir = (heelLFake.transform.position - toeL.Position).normalized;
        var rightDir = (heelRFake.transform.position - toeR.Position).normalized;


        rightFootMesh.transform.position = toeR.Position;
        leftFootMesh.transform.position = toeL.Position;

        rightFootMesh.transform.forward = rightDir;
        leftFootMesh.transform.forward = leftDir;

        /* 

         //Add overall.
       

       

        

         rightFootMesh.transform.position = toeR.Position;
         leftFootMesh.transform.position = toeL.Position;*/

    }







}
