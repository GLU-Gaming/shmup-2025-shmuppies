using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Player Stats")]
    private float baseSpeed; // Speed of the player at the start of the game
    public float playerSpeed = 4f; // Player's speed stat
    public float focusSlowdown = 2f; // How much player slows down

    private bool isMoving = false; // For testing

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Automatically grabs Rigidbody on gameObject for reference
        baseSpeed = playerSpeed; // Store initial speed
    }

    private void FixedUpdate()
    {
        LeftRightMovement();
        MoveForward();
    }

    private void Update()
    {
        // Slow down when holding Mouse0, return to normal when released
        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerSpeed = baseSpeed / 2; // Apply slowdown
        }
        else
        {
            playerSpeed = baseSpeed; // Reset speed
        }

    }

    void LeftRightMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * playerSpeed;
        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, rb.linearVelocity.z);
    }

    void MoveForward()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerSpeed);
        }
    }
}
