using UnityEngine;

public class rotateEnemy : MonoBehaviour
{
    protected GameObject player;
    protected Rigidbody rb;

    [Header("Stats")]
    public float speed = 3f;
    public float xpDropped = 25f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    protected virtual void FixedUpdate()
    {
        if (player == null) return;

        transform.LookAt(player.transform.position);
        rb.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(currentRotation.x, -90, currentRotation.z);
    }
}
