using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelLocker : MonoBehaviour
{

    GameObject levelOneButton;
    GameObject levelTwoButton;
    GameObject levelThreeButton;

    // Start is called before the first frame update
    void Start()
    {
        levelOneButton = GameObject.Find("btn_lvl1");
        levelTwoButton = GameObject.Find("btn_lvl2");
        levelThreeButton = GameObject.Find("btn_lvl3");

        Debug.Log(levelOneButton);
        if (PlayerPrefs.GetString("MaxCompletedLevel") == "1")
        {
            levelTwoButton.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetString("MaxCompletedLevel") == "2")
        {
            levelTwoButton.GetComponent<Button>().interactable = true;
            levelThreeButton.GetComponent<Button>().interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
