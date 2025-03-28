using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public Image xpBar;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;

    private Health playerHealth;
    private XPManager playerXP;


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
}
