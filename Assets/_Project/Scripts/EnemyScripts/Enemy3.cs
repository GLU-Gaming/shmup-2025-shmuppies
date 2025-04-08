using UnityEngine;

public class Enemy3 : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 3.5f;
        xpDropped = 75f;
        
    }


    protected override void FixedUpdate()
    {
        if (player == null) return;

       
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

       
        transform.rotation = Quaternion.Euler(-90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

        
        rb.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }

}