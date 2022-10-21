using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenuScreen()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ToModeSelectScreen()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ToTutorial()
    {
        Debug.Log("Going to Tutorial!");
    }

    public void ToLevelSelectScreen(string mode)
    {
        if (Equals(mode, "free"))
        {
            PlayerPrefs.SetString("Mode", "free");
        }
        if (Equals(mode, "story"))
        {
            PlayerPrefs.SetString("Mode", "story");
        }
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ToMainGameScreen(string level)
    {
        int diff = GameObject.Find("LevelDifficultyDropDown").GetComponent<TMP_Dropdown>().value;
        if (diff == 0)
        {
            PlayerPrefs.SetString("Difficulty", "Easy");
        }
        if (diff == 1)
        {
            PlayerPrefs.SetString("Difficulty", "Medium");
        }
        if (diff == 2)
        {
            PlayerPrefs.SetString("Difficulty", "Hard");
        }

        if (Equals(level, "1"))
        {
            PlayerPrefs.SetString("Level", "1");
        }
        if (Equals(level, "2"))
        {
            PlayerPrefs.SetString("Level", "2");
        }
        if (Equals(level, "3"))
        {
            PlayerPrefs.SetString("Level", "3");
        }
        if (Equals(PlayerPrefs.GetString("Mode"), "free"))
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        else if (Equals(PlayerPrefs.GetString("Mode"), "story"))
        {
            if (Equals(level, "1"))
            {
                SceneManager.LoadScene(5, LoadSceneMode.Single);
            }
            if (Equals(level, "2"))
            {
                SceneManager.LoadScene(6, LoadSceneMode.Single);
            }
            if (Equals(level, "3"))
            {
                SceneManager.LoadScene(7, LoadSceneMode.Single);
            }
        }
    }

    public void ToHighScores()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void SetDifficulty(string diff)
    {
        PlayerPrefs.SetString("Difficulty", diff);
    }
}
