using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUIHanlder : MonoBehaviour
{
    public TMP_InputField PlayerNameInputField;

    public void OnPlayerNameChanged()
    {
        GameManager.Instance.SetPlayerName(PlayerNameInputField.text);
    }
}
