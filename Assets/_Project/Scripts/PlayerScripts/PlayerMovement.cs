using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Movement")]
    public float playerSpeed = 4f;   
    public float acceleration = 2f; 
    public float deceleration = 1.2f;

    [Header("Rotation")]
    public float turnSpeed = 100f;   
    public float turnAcceleration = 3f; 
    public float turnDeceleration = 4f; 

    private float currentSpeed = 0f; 
    private float currentTurnSpeed = 0f; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
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
           
            currentSpeed = Mathf.MoveTowards(currentSpeed, playerSpeed, playerSpeed * acceleration * Time.fixedDeltaTime);
        }
        else
        {
           
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, playerSpeed * deceleration * Time.fixedDeltaTime);
        }

        
        rb.linearVelocity = transform.forward * currentSpeed;
    } 

    void Steering()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = -1f; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationInput = 1f; 
        }

        if (rotationInput != 0)
        {
            
            currentTurnSpeed = Mathf.MoveTowards(currentTurnSpeed, rotationInput * turnSpeed, turnSpeed * turnAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            
            currentTurnSpeed = Mathf.MoveTowards(currentTurnSpeed, 0f, turnSpeed * turnDeceleration * Time.fixedDeltaTime);
        }

        
        transform.Rotate(0, currentTurnSpeed * Time.fixedDeltaTime, 0);
    }
}
