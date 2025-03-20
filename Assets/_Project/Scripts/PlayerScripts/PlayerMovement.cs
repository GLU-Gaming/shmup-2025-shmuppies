using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Player Stats")]
    public float playerSpeed = 4f; // Player's speed stat
    public float turnSpeed = 5f;

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
            rb.linearVelocity = transform.forward * playerSpeed;
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0); // Stop forward movement when W is not pressed
        }
    }

    void Steering()
    {
        float rotationInput = 0f;

        // Check if A or D is pressed
        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = 1f; // Rotate counterclockwise (left)
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationInput = -1f; // Rotate clockwise (right)
        }

        // Apply rotation to the object
        transform.Rotate(0, rotationInput * turnSpeed * Time.deltaTime, 0); // Rotate around the Y-axis
    }
}
