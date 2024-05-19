using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreController scoreController;

    void Update()
    {
        scoreText.text = "Score: " + scoreController.currentScore.ToString();
        Debug.Log(scoreController.currentScore.ToString());
    }
}
