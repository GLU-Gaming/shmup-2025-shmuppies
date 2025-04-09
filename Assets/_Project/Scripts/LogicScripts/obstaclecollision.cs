using UnityEngine;

public class obstaclecollision : MonoBehaviour
{
    public float damage;

    public Health healthScript;
    void Start()
    {
        
       healthScript = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthScript.TakeDamage(damage);
            Destroy(gameObject);
        } else
        {
            return;
        }

    }




}
