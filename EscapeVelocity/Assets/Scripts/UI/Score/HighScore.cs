using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScore : ScoreCollection
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool reset;

    [SerializeField] float highScore;
    [SerializeField] float score;

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
        score = time;
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
            score = 0f;
            highScore = 0f;
            reset = false; // so it doesn't keep resetting
        }
    }

    private void LoadHighScore()
    {   score = PlayerPrefs.GetFloat("Score", 0f);
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {Mathf.FloorToInt(score)}";
        highScoreText.text = $"Highscore: {Mathf.FloorToInt(highScore)}";
    }
}

