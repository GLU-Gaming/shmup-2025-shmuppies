using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;            // Initial score
    public TextMeshProUGUI scoreText;   // Reference to the TextMeshProUGUI component

    void Update()
    {
        // Increment score by Time.deltaTime every frame
        score += 10 * Time.deltaTime;

        // Update the score text with the current score (formatted to 2 decimal places)
        scoreText.text = "Score: " + score.ToString("F0");
    }
}
