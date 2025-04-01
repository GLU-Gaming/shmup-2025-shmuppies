using UnityEngine;

public class enemy2behaviour : MonoBehaviour
{
    public GameObject player;

    [Header("References")]
    public Rigidbody rb;

    [Header("Stats")]
    public float speed = 5f;

    [Header("XP")]
    public float xpDropped = 25f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");

    }


    void FixedUpdate()
    {
        gameObject.transform.LookAt(player.transform.position, Vector3.up);
        rb.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }
}
