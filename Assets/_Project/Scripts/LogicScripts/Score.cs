using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        score = 0;
        scoreText.text = "score" + score;
    }

    public void AddScore(int amount)
    {
        score = score + amount;

        scoreText.text = "score:" + score;
    }

}

