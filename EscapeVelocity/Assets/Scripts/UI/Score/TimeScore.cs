using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeScore : ScoreManager
{
    [SerializeField] TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        SurvivalTimer();
    }

    private void SurvivalTimer()
    {
        time += Time.deltaTime;
        scoreText.text = "Score:" + Mathf.Floor(time).ToString();
    }
}
