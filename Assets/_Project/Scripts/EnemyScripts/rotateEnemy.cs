using UnityEngine;

public class RotateEnemy : MonoBehaviour
{
    public GameObject player;

    protected Rigidbody rb;
    public Vector3 spawnPosition;

    [Header("Stats")]
    public float speed = 3f;
    public float xpDropped = 25f;

    void Start()
    {
        
        
    }

    void Update()
    {
        if (player != null)
        {
            
            Vector3 targetPosition = player.transform.position;
            targetPosition.x = transform.position.x; 

           
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

          
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.x = 0; 

            
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Euler(-90f, targetRotation.eulerAngles.y, 0f); 
        }
    }
}

