using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public float speed = 3f;
    public float attackCooldown = 3f;

    private float attackTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        attackTimer = attackCooldown;
    }

    void Update()
    {
        FollowPlayer();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            ChooseAttack();
            attackTimer = attackCooldown;
        }
    }

    void FollowPlayer()
    {
        if (player == null) return;

        transform.LookAt(player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void ChooseAttack()
    {
        int attackType = Random.Range(0, 3); // Change this number to match the number of attacks you want
        switch (attackType)
        {
            case 0:
                BroadsideCannon();
                break;
            case 1:
                GhostlyWail();
                break;
            case 2:
                SummonSpecters();
                break;
        }
    }

    void BroadsideCannon()
    {
        // Add logic to fire cannons from the sides
        Debug.Log("Boss uses Broadside Cannon!");
    }

    void GhostlyWail()
    {
        // AoE or screen-shaking attack
        Debug.Log("Boss uses Ghostly Wail!");
    }

    void SummonSpecters()
    {
        // Spawns ghost minions
        Debug.Log("Boss summons Specters!");
    }
}
