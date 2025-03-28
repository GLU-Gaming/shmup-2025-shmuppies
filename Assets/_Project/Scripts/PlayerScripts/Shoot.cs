using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject bullet;
    public float fireRate = 0.2f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime; // Accumulate time

        // Check if the mouse is being held down and if enough time has passed
        if (Input.GetKey(KeyCode.Mouse0) && timer >= fireRate)
        {
            Fire();
            timer = 0f; // Reset the timer after firing
        }
    }

    void Fire()
    {
        // Fire from each fire point
        foreach (Transform point in firePoints)
        {
            Instantiate(bullet, point.position, point.rotation); // Instantiate bullet at each fire point
        }
    }
}
