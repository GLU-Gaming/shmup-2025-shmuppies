using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthUIManager : MonoBehaviour
{
    [Header("Boss Health UI Elements")]
    public Image bossHealthBar;  // Reference to the boss health bar
    public TextMeshProUGUI bossHealthText;  // Reference to the boss health text
    private Health bossHealth;  // The health component of the boss

    private void Update()
    {
        // Continuously check for a GameObject with the "Boss" tag and get its Health component
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null)
        {
            bossHealth = boss.GetComponent<Health>();
            if (bossHealth != null)
            {
                ShowBossHealth(bossHealth); // Show and update health UI if the boss is found
                UpdateBossHealth(bossHealth); // Update the health bar continuously
            }
        }
        else
        {
            HideBossHealth(); // Hide the health bar if no boss is found
        }
    }

    // Call this method to show the boss health UI when the boss spawns
    public void ShowBossHealth(Health bossHealth)
    {
        if (bossHealth != null)
        {
            bossHealthBar.gameObject.SetActive(true); // Make the boss health bar visible
            bossHealthText.gameObject.SetActive(true); // Make the boss health text visible
        }
    }

    // Call this method to update the boss health bar while the boss is alive
    public void UpdateBossHealth(Health bossHealth)
    {
        if (bossHealth != null)
        {
            bossHealthBar.fillAmount = bossHealth.maxHealth > 0 ? bossHealth.currentHealth / bossHealth.maxHealth : 0;
            bossHealthText.text = Mathf.RoundToInt(bossHealth.currentHealth).ToString();
        }
    }

    // Call this method when the boss dies to hide the health bar
    public void HideBossHealth()
    {
        bossHealthBar.gameObject.SetActive(false); // Hide the boss health bar
        bossHealthText.gameObject.SetActive(false); // Hide the boss health text
    }
}
