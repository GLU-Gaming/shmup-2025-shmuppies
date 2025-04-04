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

    private void PlayerDeath()
    {
        SceneManager.LoadScene("EndScene");
    }
}
