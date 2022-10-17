using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ToModeSelectScreen()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ToTutorial()
    {
        Debug.Log("Going to Tutorial!");
    }

    public void ToLevelSelectScreen()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void ToMainGameScreen(string level)
    {
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
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SetDifficulty(string diff)
    {
        PlayerPrefs.SetString("Difficulty", diff);
    }

    public void unpause()
    {
        string prevScreen = PlayerPrefs.GetString("PreviousScreenPaused");

    }
}
