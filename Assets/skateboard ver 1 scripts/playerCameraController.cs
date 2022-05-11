using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCameraController : MonoBehaviour
{
    [SerializeField] private float lookSensitiivty;
    [SerializeField] private float smoothing;
    [SerializeField] private int maxLookRotation = 80;

    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPosition;


    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); //Takes the raw values of the X and Y position of our cursor, I think the difference from where they used to be specifically

        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitiivty * smoothing, lookSensitiivty * smoothing)); // We scale our input values by our looksensitivity times our smoothing

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing); // Linearly interpolate how much our camera is going to pivot by a smoothing value. Makes it less jumpy when you move around
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPosition += smoothedVelocity;
        currentLookingPosition.y = Mathf.Clamp(currentLookingPosition.y, -maxLookRotation, maxLookRotation);

        // your character is very drunk mode:
        //transform.Rotate(-currentLookingPosition.y * 0.003f, currentLookingPosition.x * 0.003f, 0);
        
        
        transform.localRotation = Quaternion.AngleAxis(-currentLookingPosition.y, Vector3.right) * Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up); //This let's the player look up and down
        //player.transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up);
        //transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up);
    }

}
