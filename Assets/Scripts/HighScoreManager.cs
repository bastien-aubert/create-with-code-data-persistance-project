using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    public string currentPlayerName;
    public string highScorePlayerName;
    public int currentHighScore;

    private string fileDataPath;

    class HighScoreData
    {
        public string currentPlayerName;
        public string highScorePlayerName;
        public int highScore;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        fileDataPath = Application.persistentDataPath + "/high_score.json";

        LoadHighScoreFile();
    }

    public void LoadHighScoreFile()
    {
        if (File.Exists(fileDataPath))
        {
            string highScoreDataFileContent = File.ReadAllText(fileDataPath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(highScoreDataFileContent);
            currentPlayerName = data.currentPlayerName;
            currentHighScore = data.highScore;
            highScorePlayerName = data.highScorePlayerName;
        }
    }

    public void SaveHighScoreFile()
    {
        HighScoreData data = new HighScoreData();
        data.currentPlayerName = currentPlayerName;
        data.highScore = currentHighScore;
        data.highScorePlayerName = highScorePlayerName;

        string fileContent = JsonUtility.ToJson(data);
        File.WriteAllText(fileDataPath, fileContent);
    }

    public void UpdateHighScore(int currentScore)
    {
        if (currentScore > currentHighScore)
        {
            currentHighScore = currentScore;
            highScorePlayerName = currentPlayerName;
            SaveHighScoreFile();
        }
    }

    public string GenerateHighScoreString()
    {
        return "Best score: " + HighScoreManager.instance.highScorePlayerName + " - " + HighScoreManager.instance.currentHighScore;
    }
}
