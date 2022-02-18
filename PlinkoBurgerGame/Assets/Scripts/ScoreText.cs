using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    void Start()
    {
        scoreText.text = "0";
    }

    void Update()
    {
        scoreText.text = Score.S.playerScore.ToString();
    }
}