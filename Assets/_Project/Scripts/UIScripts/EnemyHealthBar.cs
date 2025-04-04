using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBarFill;
    public Canvas healthCanvas;
    public GameObject healthBar;  // Health bar GameObject (Canvas)

    private Transform enemy;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        enemy = transform.parent; // Assumes this script is attached to a child UI Canvas
        transform.SetParent(null, true); // Unparent it but keep world position
        Hide(); // Hide on start
    }

    private void LateUpdate()
    {
        if (enemy != null)
        {
            // Position the health bar above the enemy
            transform.position = enemy.position + new Vector3(0, 2f, 0);

            // Make the health bar face the camera
            transform.rotation = mainCamera.transform.rotation;
        }
    }

    public void SetHealthBar(float currentHealth, float maxHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void Show()
    {
        healthCanvas.enabled = true;
    }

    public void Hide()
    {
        healthCanvas.enabled = false;
    }
}
