using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody rb;
    public float damage = 10f;
    public float lifeTime = 5f;
    [SerializeField] private float forwardSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * forwardSpeed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
