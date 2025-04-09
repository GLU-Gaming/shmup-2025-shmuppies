using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager instance;

    public float currentXP = 0;
    public int level = 1;
    public float xpToNextLevel = 100;

    public int skillPoints = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddXP(float amount)
    {
        currentXP += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();

        }
    }

    public void LevelUp()
    {
        level++;
        skillPoints += 2;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f); // Increase required XP each level
    }

    public void BuyUpgrade()
    {
        skillPoints--;
    }
}
