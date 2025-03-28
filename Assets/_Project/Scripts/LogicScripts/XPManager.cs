using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager instance;

    public float currentXP = 0;
    public int level = 1;
    public float xpToNextLevel = 100;

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
            level++;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f); // Increase required XP each level
            Debug.Log("Leveled up! New level: " + level);
        }
    }
}
