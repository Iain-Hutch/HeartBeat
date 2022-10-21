using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneEnder : MonoBehaviour
{
    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        string level = PlayerPrefs.GetString("Level");

        if (time > 18.0f && Equals(level, "1")) {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        if (time > 9.3f && Equals(level, "2")) {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        if (time > 6.0f && Equals(level, "3")) {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }
}
