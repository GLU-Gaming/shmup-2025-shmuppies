using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody rb;
    public float lifeTime = 5f;
    [SerializeField] private float forwardSpeed;
        
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * forwardSpeed;
        Destroy(gameObject, lifeTime);
    }   
    //
}
