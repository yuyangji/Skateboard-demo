using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 7;
    private float jumpTolerance = 1.6f;
    [SerializeField] private float vAxisMovementSpeed = 3f;

    private float skateCooldown = 0f;
    public bool grinding = false;
    public Vector3 grindVector;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private float maxSpeed = 13f;

    private Rigidbody rb; //The reason this is called rb is because this is a naming convention. I'll use that from now on

// Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private bool isGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, jumpTolerance));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
    }

    private void Grind()
    {
        if (grinding)
        {
            if (Input.GetKey(KeyCode.Space)) {
                rb.velocity = Vector3.zero; // So that our grinding points are "sticky"
                rb.MovePosition(grindVector); // Our grind line tells us which way we want to go and which way to go back, so we just move our position according to the vector given
                if (Vector3.Distance(transform.position, grindVector) < 0.01f)
                {
                    rb.AddForce(0, jumpHeight * 1.5f, 0, ForceMode.Impulse);
                    grinding = false;
                }
            }
            else
            {
                //rb.AddForce(0, jumpHeight * 1.5f, 0, ForceMode.Impulse);
                grinding = false;
            }
        }
    }

    private void Move()
    {
        float vAxis = Input.GetAxisRaw("Vertical");

        //Vector3 movement = new Vector3(hAxis * hAxisMovementSpeed, 0, vAxis * vAxisMovementSpeed) * Time.fixedDeltaTime;
        //Vector3 movement = new Vector3(0, 0, vAxis * vAxisMovementSpeed) * Time.fixedDeltaTime;
        //Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        //rb.MovePosition(newPosition);
        if (skateCooldown < 0 && isGrounded()) {
            rb.AddRelativeForce(0, 0, vAxisMovementSpeed * vAxis, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); // Clamp our max velocity to a limit without affecting the direction
            skateCooldown = cooldown;
        }
        else { skateCooldown -= Time.fixedDeltaTime;  }
    }

    private void Rotate()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");

        transform.Rotate(0, hAxis * 2, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        Jump();
        Grind();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
}
