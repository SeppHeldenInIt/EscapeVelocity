using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : ScoreManager
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool reset;

    [SerializeField] float highScore;
    private void Start()
    {
        LoadHighScore();
        UpdateUI();
    }

    private void Update()
    {
        CheckHighScore();
        ResetHighScoreIfNeeded();
        UpdateUI();
    }

    private void CheckHighScore()
    {
        if (time > highScore)
        {
            highScore = time;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    private void ResetHighScoreIfNeeded()
    {
        if (reset)
        {
            PlayerPrefs.DeleteKey("HighScore");
            highScore = 0f;
            reset = false; // so it doesn't keep resetting
        }
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetFloat("HighScore:", 0f);
    }

    private void UpdateUI()
    {
        highScoreText.text = $"Highscore: {Mathf.FloorToInt(highScore)}";
    }
}
