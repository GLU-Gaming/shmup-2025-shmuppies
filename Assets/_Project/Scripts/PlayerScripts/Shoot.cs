using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject bullet;
    public float fireRate = 0.2f;
    private float timer = 0f;
    public AudioSource canon;

    public GameObject cannonParticles;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && timer >= fireRate)
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        foreach (Transform point in firePoints)
        {
            Instantiate(bullet, point.position, point.rotation);

            // Instantiate and store the reference to destroy it later
            GameObject spawnedParticles = Instantiate(cannonParticles, point.position, point.rotation);
            Destroy(spawnedParticles, 2f); // Correctly destroys the instantiated particles

            canon.Play();
        }
    }
}

