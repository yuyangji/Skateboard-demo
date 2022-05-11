using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using XR_Gestures;
using UnityEngine.UI;
using System;

public class GestureDebugger : MonoBehaviour
{

    [Header("Components")]

    [SerializeField] Transform MoveInfoLeft;
    [SerializeField] Transform MoveInfoRight;


    Tracker leftFoot;
    Tracker rightFoot;

    [Header("Visual settings")]
    [Space(20)]
    [SerializeField] int fontSize;
    [SerializeField] int labelSize;
    [SerializeField] Color backgroundColor;

    TextMeshProUGUI posL, posR, velL, velR;

    [Header("Advanced settings")]
    [Range(0.1f, 1f)]
    [SerializeField] public float updateSeconds = 0.1f;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            if (text.transform.name == "label")
            {
                text.fontSizeMax = labelSize;
                text.fontSizeMin = labelSize;
            }
            else
            {
                text.fontSizeMax = fontSize;
                text.fontSizeMin = fontSize;
            }

        }
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.transform.name == "background")
            {
                image.color = backgroundColor;
            }
        }

    }

    private void Awake()
    {

      /*  leftFoot = manager.leftFoot;
        rightFoot = manager.rightFoot;*/

        try
        {
            posL = MoveInfoLeft.Find("pos_L").GetComponent<TextMeshProUGUI>();
            velL = MoveInfoLeft.Find("vel_L").GetComponent<TextMeshProUGUI>();
            posR = MoveInfoRight.Find("pos_R").GetComponent<TextMeshProUGUI>();
            velR = MoveInfoRight.Find("vel_R").GetComponent<TextMeshProUGUI>();
        }
        catch (NullReferenceException err)
        {
            Debug.LogError(err.ToString());
        }
        StartCoroutine(UpdateText());
    }

    void UpdateDeltas()
    {
        try
        {
            posL.SetText(leftFoot.Position.ToString());
            velL.SetText(leftFoot.Velocity.ToString());
            posR.SetText(rightFoot.Position.ToString());
            velR.SetText(rightFoot.Velocity.ToString());
        }
        catch (NullReferenceException err)
        {
            Debug.LogError(err.ToString());
        }

    }


    private string GenerateText(Tracker _tracker)
    {

        string text = $"Heel Grounded: {_tracker.IsOnGround()} \nToe Grounded: {_tracker.IsOnGround()}\n";

        text += $"\n {(System.Math.Round(_tracker.Position.x, 2), (System.Math.Round(_tracker.Position.y, 2)), (System.Math.Round(_tracker.Position.z, 2)))}";
        text += $"\n velocity : {_tracker.Velocity}";

        return text;
    }



    IEnumerator UpdateText()
    {
        UpdateDeltas();
        while (true)
        {

            yield return new WaitForSeconds(updateSeconds);
        }

    }




}
