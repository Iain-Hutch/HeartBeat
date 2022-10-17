using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresPopulator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int easyOne = PlayerPrefs.GetInt("Easy1");
        int mediumOne = PlayerPrefs.GetInt("Medium1");
        int hardOne = PlayerPrefs.GetInt("Hard1");
        int easyTwo = PlayerPrefs.GetInt("Easy2");
        int mediumTwo = PlayerPrefs.GetInt("Medium2");
        int hardTwo = PlayerPrefs.GetInt("Hard2");
        int easyThree = PlayerPrefs.GetInt("Easy3");
        int mediumThree = PlayerPrefs.GetInt("Medium3");
        int hardThree = PlayerPrefs.GetInt("Hard3");

        Text text = GameObject.Find("highScoresText").GetComponent<Text>();
        text.text = easyOne + "\n" + mediumOne + "\n" + hardOne + "\n\n" + easyTwo + "\n" + mediumTwo + "\n" + hardTwo + "\n\n" + easyThree + "\n" + mediumThree + "\n" + hardThree;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
