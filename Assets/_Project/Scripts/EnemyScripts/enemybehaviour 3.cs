using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class enemybehaviour3 : MonoBehaviour
{
    public GameObject player;

    [Header("References")]
    public Rigidbody rb;

    [Header("Stats")]
    public float speed = 3f;

    [Header("XP")]
    public float xpDropped = 25f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        

    }


    void FixedUpdate()
    {
       

        gameObject.transform.LookAt(player.transform.position);
        gameObject.transform.rotation *= Quaternion.Euler(-90, 0, 0);

        rb.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }
}
