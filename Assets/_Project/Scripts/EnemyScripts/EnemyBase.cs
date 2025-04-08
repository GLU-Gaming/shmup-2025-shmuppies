using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected GameObject player;
    protected Rigidbody rb;

    public float speed;
    public float xpDropped;

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
    }
}
