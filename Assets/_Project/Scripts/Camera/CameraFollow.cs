using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // The player to follow
    public float smoothSpeed = 0.125f; // Speed of smoothing
    public Vector3[] cameraOffsets; // Array of different camera offsets
    private int currentViewIndex = 0; // Current camera view index
    public float followDelay = 0.3f; // Time before camera starts moving after the player

    private Vector3 velocity = Vector3.zero;
    private float currentDelayTime = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // Ensure at least one camera offset is set
        if (cameraOffsets.Length == 0)
        {
            cameraOffsets = new Vector3[]
            {
                new Vector3(0, 20, -15), // Default
                new Vector3(0, 20, -2),  // Top-down
                new Vector3(0, 15, -25)  // Front View
            };
        }
    }

    void Update()
    {
        if (player != null)
        {
            currentDelayTime += Time.deltaTime;

            if (currentDelayTime >= followDelay)
            {
                FollowPlayer();
            }

            // Check if "1" key is pressed to switch camera view
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchCameraView();
            }
        }
    }

    void FollowPlayer()
    {
        // Rotate the offset based on the player's rotation
        Vector3 rotatedOffset = player.transform.rotation * cameraOffsets[currentViewIndex];

        // Get the new desired position
        Vector3 desiredPosition = player.transform.position + rotatedOffset;

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Make the camera look at the player
        transform.LookAt(player.transform);
    }

    void SwitchCameraView()
    {
        currentViewIndex = (currentViewIndex + 1) % cameraOffsets.Length;
    }
}
