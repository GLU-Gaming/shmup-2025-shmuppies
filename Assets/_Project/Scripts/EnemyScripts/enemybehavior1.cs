using UnityEngine;

public class enemymovement : MonoBehaviour
{
    public GameObject player;
   
    private Rigidbody rb;
    public float speed = 4f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

 
    void FixedUpdate()
    {
        gameObject.transform.LookAt(player.transform.position, Vector3.up);
        rb.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }
}
