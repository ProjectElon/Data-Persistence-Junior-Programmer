using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField PlayerNameInputField;

    public TMP_Text BestScoreText;

    void Start()
    {
        BestScoreText.text = GameManager.Instance.GetPlayerBestScore();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnPlayerNameChanged()
    {
        GameManager.Instance.SetPlayerName(PlayerNameInputField.text);
    }
}
