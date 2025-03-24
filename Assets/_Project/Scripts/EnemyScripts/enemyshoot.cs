using JetBrains.Annotations;
using UnityEngine;

public class enemyshoot : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    public Transform firePoint;
    public float detectionRange;
    public LayerMask playerLayer;

    private float timer = 0f;  // Persisting the timer across frames.

    void Update()
    {
        // Detect players within the detection range
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);

        // If there are any players detected
        if (colliders.Length > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Increment timer by the time passed since the last frame
        timer += Time.deltaTime;

        // If the timer exceeds the fire rate, shoot the bullet and reset the timer
        if (timer >= fireRate)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);  // Properly set position and rotation
            timer = 0f;  // Reset the timer after shooting
        }
    }
}
