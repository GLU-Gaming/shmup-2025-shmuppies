using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // The player to follow
    public float smoothSpeed = 0.125f; // Speed of smoothing
    public Vector3 offset; // Offset from the player
    public float followDelay = 0.3f; // Time before camera starts moving after the player

    private Vector3 velocity = Vector3.zero;
    private float currentDelayTime = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        }
    }

    void FollowPlayer()
    {
        // Rotate the offset based on the player's rotation
        Vector3 rotatedOffset = player.transform.rotation * offset;

        // Get the new desired position
        Vector3 desiredPosition = player.transform.position + rotatedOffset;

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Make the camera look at the player
        transform.LookAt(player.transform);
    }
}

