using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // The player to follow
    public float smoothSpeed = 0.125f; // Speed of smoothing
    public Vector3 offset; // Offset from the player (e.g., position of camera relative to player)
    public float followDelay = 0.3f; // Time before camera starts moving after the player (in seconds)

    private Vector3 velocity = Vector3.zero; // For smooth movement (used by SmoothDamp)
    private float currentDelayTime = 0f;

    void Start()
    {
        // Check if the player is assigned
        if (player == null)
        {
            Debug.LogError("Player not assigned!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            currentDelayTime += Time.deltaTime;

            if (currentDelayTime >= followDelay)
            {
                // After the delay, make the camera follow the player smoothly
                FollowPlayer();
            }
        }
    }

    void FollowPlayer()
    {
        // Get the target position (player position + offset)
        Vector3 desiredPosition = player.transform.position + offset;

        // Smoothly move the camera to the desired position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Optional: Camera looks at the player
        transform.LookAt(player.transform);
    }
}
