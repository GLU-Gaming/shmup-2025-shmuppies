using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float forwardspeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity= transform.forward*forwardspeed;
    }

 
    void Update()
    {
        
    }
}
