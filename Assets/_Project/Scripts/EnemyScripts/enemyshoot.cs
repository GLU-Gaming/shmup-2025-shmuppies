using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Stats")]
    public GameObject bullet;
    public float fireRate;

    [Header("References")]
    public Transform firePoint;

    [Header("Range")]
    public float detectionRange;
    public LayerMask playerLayer;

    private float timer = 0f;  

    void Update()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);

       
        if (colliders.Length > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
      
        timer += Time.deltaTime;

       
        if (timer >= fireRate)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation); 
            timer = 0f;  
        }
    }
}
