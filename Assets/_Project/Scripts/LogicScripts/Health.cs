using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public float healthRegen = 1f;
    public GameObject player;
    public EnemyBehaviour enemyScript;
    public EnemySpawner spawnerScript;
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");
        enemyScript = GameObject.FindWithTag("Enemy").GetComponent<EnemyBehaviour>();
        spawnerScript = FindFirstObjectByType<EnemySpawner>();
    }

    private void Update()
    {
        RegenerateHealth();

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            EnemyDeath();

            if (player.GetComponent<Health>().currentHealth <= 0)
            {
                PlayerDeath();
            }
        }
    }

    void RegenerateHealth()
    {
        currentHealth += healthRegen * Time.deltaTime;
    }

    public void EnemyDeath()
    {
        XPManager.instance.AddXP(enemyScript.xpDropped);
        spawnerScript.currentEnemyCount--;
        Destroy(gameObject);
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene("EndScene");
    }
}
