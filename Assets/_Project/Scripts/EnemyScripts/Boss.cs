using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public float speed = 3f;
    public float attackCooldown = 3f;
    public GameObject bossBullet;
    public Transform[] firePoints;
    public Transform ghostSpawn;
    public AudioSource canonshoot;
    public GameObject ghostExplosionPrefab; // Prefab for the Ghostly Wail explosion
    public GameObject homingProjectilePrefab; // Prefab for the Summon Ghost homing projectile
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionDamage = 30f; // Damage dealt by the explosion

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
                GhostExplosion();
                break;
            case 2:
                SummonGhost();
                break;
        }
    }

    void BroadsideCannon()
    {
        foreach (Transform firePoint in firePoints)
        {
            // Instantiate the bossBullet at the position and rotation of the firePoint
            Instantiate(bossBullet, firePoint.position, firePoint.rotation);
        }
        canonshoot.Play();
    }

    void GhostExplosion()
    {
        // Create an explosion after a brief delay
        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        // Wait for a short time (2 seconds) before the explosion
        yield return new WaitForSeconds(2f);

        // Instantiate the ghost explosion at the boss's position
        GameObject explosion = Instantiate(ghostExplosionPrefab, transform.position, Quaternion.identity);

        // Damage players within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Deal damage to the player
                Health playerHealth = hitCollider.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(explosionDamage); // Assuming TakeDamage is a method in the player's Health script
                }
            }
        }

        // Optionally, destroy the explosion after some time
        Destroy(explosion, 2f); // Destroy the explosion object after 2 seconds
    }

    void SummonGhost()
    {
        // Instantiate a homing projectile that targets the player
        if (player != null)
        {
            GameObject homingProjectile = Instantiate(homingProjectilePrefab, ghostSpawn.position, Quaternion.identity);
            HomingProjectile homingScript = homingProjectile.GetComponent<HomingProjectile>();

            if (homingScript != null)
            {
                homingScript.target = player; // Set the player as the target for the homing projectile
            }
        }
    }
}
