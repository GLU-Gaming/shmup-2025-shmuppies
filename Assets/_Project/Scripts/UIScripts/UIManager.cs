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
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene change
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        menuActive = false;

        // Get player and shooter references
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Ensure player has all required components
            playerMovement = player.GetComponent<PlayerMovement>();
            shootScript = player.GetComponent<Shoot>();
            playerHealth = player.GetComponent<Health>();

            // If player doesn't have necessary components, log an error
            if (playerMovement == null || shootScript == null || playerHealth == null)
            {
                Debug.LogError("Player is missing necessary components: " +
                    $"{(playerMovement == null ? "PlayerMovement " : "")}" +
                    $"{(shootScript == null ? "Shoot " : "")}" +
                    $"{(playerHealth == null ? "Health" : "")}");
            }
        }

        playerXP = GameObject.FindWithTag("XPManager")?.GetComponent<XPManager>();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthBar.fillAmount = playerHealth.maxHealth > 0 ? playerHealth.currentHealth / playerHealth.maxHealth : 0;
        healthText.text = Mathf.RoundToInt(playerHealth.currentHealth).ToString();
        xpBar.fillAmount = playerXP.currentXP > 0 ? playerXP.currentXP / playerXP.xpToNextLevel : 0;
        xpText.text = Mathf.RoundToInt(playerXP.currentXP).ToString();
        levelText.text = Mathf.RoundToInt(playerXP.level).ToString();
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

    // Upgrade functions
    public void UpgradeDamage()
    {
        if (shootScript != null)
        {
            foreach (var shoot in shootScript.GetComponentsInChildren<Shoot>())
            {
                var bulletPrefab = shoot.bullet.GetComponent<bullet>();
                bulletPrefab.damage += 30f; // Increase base damage
            }
        }
        else
        {
            Debug.LogError("Shoot script not found on player.");
        }
    }

    public void UpgradeSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.playerSpeed += 5f;
        }
        else
        {
            Debug.LogError("PlayerMovement script not found on player.");
        }
    }

    public void UpgradeFireRate()
    {
        if (shootScript != null)
        {
            foreach (var shoot in shootScript.GetComponentsInChildren<Shoot>())
            {
                shoot.fireRate = Mathf.Max(0.05f, shoot.fireRate - 0.03f); // Decrease interval to shoot faster
            }
        }
        else
        {
            Debug.LogError("Shoot script not found on player.");
        }
    }

    public void UpgradeHealth()
    {
        if (playerHealth != null)
        {
            playerHealth.maxHealth += 20f;
            playerHealth.currentHealth = playerHealth.maxHealth;
        }
        else
        {
            Debug.LogError("Health script not found on player.");
        }
    }

    public void UpgradeHealthRegen()
    {
        if (playerHealth != null)
        {
            playerHealth.healthRegen += 0.5f;
        }
        else
        {
            Debug.LogError("Health script not found on player.");
        }
    }
}

