using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUiHandler : MonoBehaviour
{
    TMP_InputField nameInputField;

    // Start is called before the first frame update
    void Start()
    {
        DisplayHighScore();
        LoadLastPlayerName();
    }

    void DisplayHighScore()
    {
        TMP_Text highScoreText = GameObject.Find("BestScoreText").GetComponent<TMP_Text>();
        if (HighScoreManager.instance.highScorePlayerName != "")
        {
            highScoreText.text = HighScoreManager.instance.GenerateHighScoreString();
        }
    }

    void LoadLastPlayerName()
    {
        nameInputField = GameObject.Find("PlayerNameInput").GetComponent<TMP_InputField>();
        nameInputField.text = HighScoreManager.instance.currentPlayerName;
        nameInputField.onValueChanged.AddListener(delegate { EditPlayerName(); });
    }

    void EditPlayerName()
    {
        HighScoreManager.instance.currentPlayerName = nameInputField.text;
        HighScoreManager.instance.SaveHighScoreFile();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
