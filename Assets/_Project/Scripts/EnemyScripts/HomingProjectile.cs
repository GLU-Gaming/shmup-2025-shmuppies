using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public GameObject target; // The target object (player)
    public float speed = 5f; // Speed of the projectile

    void Update()
    {
        if (target != null)
        {
            // Move towards the target (player)
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;


            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else
        {
            // Destroy the projectile if no target is assigned
            Destroy(gameObject);
        }
    }

    // If you want the projectile to deal damage upon collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f);
            }

            Destroy(gameObject); // Destroy the projectile after hitting the player
        }
    }

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        Destroy(gameObject, 10f);
    }

    
}
