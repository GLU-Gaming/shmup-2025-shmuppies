using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public float healthRegen = 1f;

    private EnemySpawner spawnerScript;

    [Header("Health Bar UI")]
    public EnemyHealthBar healthBar; // Reference to UI script

    public static event Action OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar?.SetHealthBar(currentHealth, maxHealth);

        if (CompareTag("Enemy"))
        {
            spawnerScript = FindFirstObjectByType<EnemySpawner>();
            healthBar?.Hide();
        }
    }

    private void Update()
    {
        if (currentHealth < maxHealth)
        {
            RegenerateHealth();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke();

        if (healthBar != null)
        {
            healthBar.SetHealthBar(currentHealth, maxHealth);
            healthBar.Show();
        }

        if (currentHealth <= 0)
        {
            if (CompareTag("Player"))
            {
                PlayerDeath();
            }
            else if (CompareTag("Enemy"))
            {
                EnemyDeath();
            }
        }
    }

    private void RegenerateHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth += healthRegen * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            OnHealthChanged?.Invoke();

            if (healthBar != null)
            {
                healthBar.SetHealthBar(currentHealth, maxHealth);

                if (currentHealth == maxHealth)
                {
                    healthBar.Hide();
                }
            }
        }
    }

    private void EnemyDeath()
    {
        // Check if the enemy is the boss
        if (CompareTag("Boss"))
        {
            BossDeath(); // Call BossDeath if the enemy is the boss
        }
        else
        {
            EnemyBase enemyScript = GetComponent<EnemyBase>();
            if (enemyScript != null)
            {
                XPManager.instance.AddXP(enemyScript.xpDropped);
            }

            if (spawnerScript != null)
            {
                spawnerScript.currentEnemyCount--;
            }

            Destroy(gameObject);
        }
    }

    private void BossDeath()
    {
        // Logic to handle boss death
        Debug.Log("The boss has been defeated!");

        // Example: Load a victory scene or trigger a special event
        SceneManager.LoadScene("VictoryScene"); // Replace with your victory scene name or actions

        // Optionally, disable the boss-related objects or gameplay elements
        // Example: Disable the boss game object to prevent further actions
        gameObject.SetActive(false);
    }

    private void PlayerDeath()
    {
        SceneManager.LoadScene("EndScene");
    }
}
