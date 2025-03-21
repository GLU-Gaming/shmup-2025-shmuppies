using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Player Stats")]
    public float playerSpeed = 4f;   // Max forward speed
    public float acceleration = 10f; // Speed up rate
    public float deceleration = 8f;  // Slow down rate

    public float turnSpeed = 100f;   // Max rotation speed
    public float turnAcceleration = 200f; // Rotation acceleration
    public float turnDeceleration = 150f; // Rotation deceleration

    private float currentSpeed = 0f; // Current forward speed
    private float currentTurnSpeed = 0f; // Current turn speed

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Automatically grabs Rigidbody on gameObject for reference
    }

    private void FixedUpdate()
    {
        MoveForward();
        Steering();
    }

    void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Accelerate towards max speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, playerSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate to 0 when W is not pressed
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }

        // Apply velocity
        rb.linearVelocity = transform.forward * currentSpeed;
    }

    void Steering()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = -1f; // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationInput = 1f; // Rotate right
        }

        if (rotationInput != 0)
        {
            // Apply turn acceleration
            currentTurnSpeed = Mathf.MoveTowards(currentTurnSpeed, rotationInput * turnSpeed, turnAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Apply turn deceleration when no key is pressed
            currentTurnSpeed = Mathf.MoveTowards(currentTurnSpeed, 0f, turnDeceleration * Time.fixedDeltaTime);
        }

        // Apply smooth rotation
        transform.Rotate(0, currentTurnSpeed * Time.fixedDeltaTime, 0);
    }
}
