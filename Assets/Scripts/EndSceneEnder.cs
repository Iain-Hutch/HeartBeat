using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneEnder : MonoBehaviour
{

    float incTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        incTime = incTime + Time.deltaTime;
        if (incTime > 5f)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
