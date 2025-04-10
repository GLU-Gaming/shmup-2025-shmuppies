using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public Image xpBar;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;
    public GameObject upgradeMenu;
    public TextMeshProUGUI[] upgradeCounters;
    public TextMeshProUGUI skillPointsText; // New

    private Health playerHealth;
    private XPManager playerXP;

    public GameObject player;
    public GameObject shooter;

    private PlayerMovement playerMovement;
    private Shoot shootScript;

    private bool menuActive;
    private bool pauseActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        menuActive = false;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            shootScript = player.GetComponent<Shoot>();
            playerHealth = player.GetComponent<Health>();

            if (playerMovement == null || shootScript == null || playerHealth == null)
            {
                Debug.LogError("Player is missing necessary components: " +
                    $"{(playerMovement == null ? "PlayerMovement " : "")}" +
                    $"{(shootScript == null ? "Shoot " : "")}" +
                    $"{(playerHealth == null ? "Health" : "")}");
            }
        }

        playerXP = GameObject.FindWithTag("XPManager")?.GetComponent<XPManager>();

        // Ensure bullet damage is set to 25 at the start
        if (shootScript != null)
        {
            foreach (var shoot in shootScript.GetComponentsInChildren<Shoot>())
            {
                var bullet = shoot.bullet.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.damage = 25f; // Set the initial damage value to 25
                }
            }
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (playerHealth != null)
        {
            healthBar.fillAmount = playerHealth.maxHealth > 0 ? playerHealth.currentHealth / playerHealth.maxHealth : 0;
            healthText.text = Mathf.RoundToInt(playerHealth.currentHealth).ToString();
        }

        if (playerXP != null)
        {
            xpBar.fillAmount = playerXP.xpToNextLevel > 0 ? playerXP.currentXP / playerXP.xpToNextLevel : 0;
            xpText.text = Mathf.RoundToInt(playerXP.currentXP).ToString();
            levelText.text = playerXP.level.ToString();
            skillPointsText.text = "Skill Points: " + playerXP.skillPoints;
        }
    }

    public void ShowUpgrades()
    {
        menuActive = !menuActive;
        upgradeMenu.SetActive(menuActive);
    }

    private Coroutine pauseCoroutine;

    public void PauseGame()
    {
        pauseActive = !pauseActive;

        if (pauseCoroutine != null)
            StopCoroutine(pauseCoroutine);

        pauseCoroutine = StartCoroutine(SmoothPauseTransition(pauseActive));
    }

    private IEnumerator SmoothPauseTransition(bool pause)
    {
        float duration = 1f;
        float start = Time.timeScale;
        float end = pause ? 0f : 1f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(start, end, t / duration);
            yield return null;
        }

        Time.timeScale = end;
        pauseCoroutine = null;
    }

    // Check if the player can spend skill point
    private bool TrySpendSkillPoint()
    {
        if (playerXP != null && playerXP.skillPoints > 0)
        {
            playerXP.BuyUpgrade();
            return true;
        }

        Debug.Log("Not enough skill points.");
        return false;
    }

    private void IncrementUpgradeCounter(int index)
    {
        if (index >= 0 && index < upgradeCounters.Length)
        {
            int currentCount = int.Parse(upgradeCounters[index].text);
            currentCount++;
            upgradeCounters[index].text = currentCount.ToString();
        }
    }

    // Upgrade functions
    public void UpgradeDamage()
    {
        if (!TrySpendSkillPoint()) return;

        if (shootScript != null)
        {
            foreach (var shoot in shootScript.GetComponentsInChildren<Shoot>())
            {
                var bulletPrefab = shoot.bullet.GetComponent<Bullet>();
                bulletPrefab.damage += 30f;
            }
        }
        else
        {
            Debug.LogError("Shoot script not found on player.");
        }

        IncrementUpgradeCounter(0);
    }

    public void UpgradeSpeed()
    {
        if (!TrySpendSkillPoint()) return;

        if (playerMovement != null)
        {
            playerMovement.playerSpeed += 5f;
        }
        else
        {
            Debug.LogError("PlayerMovement script not found on player.");
        }

        IncrementUpgradeCounter(1);
    }

    public void UpgradeFireRate()
    {
        if (!TrySpendSkillPoint()) return;

        if (shootScript != null)
        {
            foreach (var shoot in shootScript.GetComponentsInChildren<Shoot>())
            {
                shoot.fireRate -= 0.125f;
            }
        }
        else
        {
            Debug.LogError("Shoot script not found on player.");
        }

        IncrementUpgradeCounter(2);
    }

    public void UpgradeHealth()
    {
        if (!TrySpendSkillPoint()) return;

        if (playerHealth != null)
        {
            playerHealth.maxHealth += 20f;
            playerHealth.currentHealth = playerHealth.maxHealth;
        }
        else
        {
            Debug.LogError("Health script not found on player.");
        }

        IncrementUpgradeCounter(3);
    }

    public void UpgradeHealthRegen()
    {
        if (!TrySpendSkillPoint()) return;

        if (playerHealth != null)
        {
            playerHealth.healthRegen += 1f;
        }
        else
        {
            Debug.LogError("Health script not found on player.");
        }

        IncrementUpgradeCounter(4);
    }
}
