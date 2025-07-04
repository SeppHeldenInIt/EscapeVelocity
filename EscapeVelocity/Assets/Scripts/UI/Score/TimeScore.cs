using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeScore : ScoreCollection
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        SurvivalTimer();
    }

    private void SurvivalTimer()
    {
        time += Time.deltaTime;
        scoreText.text = "Score: " + Mathf.Floor(time).ToString();
    }
}
