using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    public float score;
    public TextMeshPro scoreText;

    private void Update()
    {
        score += Time.deltaTime;
        score = Mathf.Round(score);
        scoreText.text = score.ToString();
    }

}

