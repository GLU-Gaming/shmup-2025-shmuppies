using UnityEngine;

public class Enemy3 : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 3.5f;
        xpDropped = 75f; // More XP
        
    }

<<<<<<< HEAD
    protected override void FixedUpdate()
    {
        if (player == null) return;

        // Smooth rotation to face the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Set the x rotation to -90 while keeping the y and z rotations from LookAt
        transform.rotation = Quaternion.Euler(-90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

        // Move towards the player
        rb.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }

=======
    
>>>>>>> 87520962f65c59fb9422adc7584523c6561ef548
}