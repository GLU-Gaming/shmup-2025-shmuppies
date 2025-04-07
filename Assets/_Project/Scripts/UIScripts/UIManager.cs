using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEditor.Rendering.Universal;

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

    public bool menuActive;
    public bool pauseActive;


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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
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



}
