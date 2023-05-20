using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string _CurrentPlayerName = "";
    private List<PlayerData> _Players;

    private PlayerData _CurrentPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);

            _Players = new List<PlayerData>();
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        if (_CurrentPlayerName != "")
        {
            _CurrentPlayer = null;

            foreach (var player in _Players)
            {
                if (player.Name == _CurrentPlayerName)
                {
                    _CurrentPlayer = player;
                    break;
                }
            }

            if (_CurrentPlayer == null)
            {
                var NewPlayer = new PlayerData(_CurrentPlayerName, 0);
                _CurrentPlayer = NewPlayer;
                _Players.Add(NewPlayer);
            }

            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
        SaveGame();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(0);
#endif
    }

    public void LoadGame()
    {
        string SaveFilePath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(SaveFilePath))
        {
            var json = File.ReadAllText(SaveFilePath);
            var SaveData = JsonUtility.FromJson<SaveData>(json);
            _Players.AddRange(SaveData.Players);
        }
    }

    public void SaveGame()
    {
        string SaveFilePath = Application.persistentDataPath + "/savefile.json";
        var SaveData = new SaveData();
        SaveData.Players = _Players.ToArray();
        var json = JsonUtility.ToJson(SaveData);
        File.WriteAllText(SaveFilePath, json);
    }

    public void SetPlayerName(string PlayerName)
    {
        _CurrentPlayerName = PlayerName;
    }

    public void SetCurrentPlayerScore(int Score)
    {
        if (Score > _CurrentPlayer.Score)
        {
            _CurrentPlayer.Score = Score;
        }
    }

    public string GetPlayerBestScore()
    {
        int BestScoreSoFar = 0;
        string PlayerName = "";
        foreach (var player in _Players)
        {
            if (player.Score > BestScoreSoFar)
            {
                BestScoreSoFar = player.Score;
                PlayerName = player.Name;
            }
        }
        if (PlayerName != "")
        {
            string Result = PlayerName + ": " + BestScoreSoFar;
            return Result;
        }
        return "";
    }
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;

    public PlayerData(string name, int score)
    {
        Name = name;
        Score = score;
    }
};

[System.Serializable]
public class SaveData
{
    public PlayerData[] Players;
};
