using UnityEditor;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform[] firePoint;
    public GameObject bullet;
    public float fireRate;
    public float fireTime;
  
    void Start()
    {
       fireTime = fireRate;
    }

    
    void Update()
    {
        fireTime -= Time.deltaTime;
        

        if (Input.GetKeyDown(KeyCode.Space)&& fireTime <=0)
        {
            Shoot();
            fireTime = fireRate;
        }

        void Shoot()
        {
            for (int i = 0; i < firePoint.Length; i++)
            {
                Instantiate(bullet, firePoint[i].position, firePoint[i].rotation);
            }

        } //
    }
}
