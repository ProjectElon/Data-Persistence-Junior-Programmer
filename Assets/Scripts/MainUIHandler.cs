using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public Text BestScoreText;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            BestScoreText.text = "Best Score: " + GameManager.Instance.GetPlayerBestScore();
        }
    }

    public void OnScoreChanged(int NewScore)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentPlayerScore(NewScore);
            BestScoreText.text = "Best Score: " + GameManager.Instance.GetPlayerBestScore();
        }
    }
}
