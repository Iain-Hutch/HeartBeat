using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMover : MonoBehaviour
{
    // Start is called before the first frame update

    private MainScript main_script;

    void Start()
    {
        main_script = FindObjectOfType<MainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!main_script.gameOver)
        {
            transform.Translate(0, 1.2f * Time.deltaTime, 0);
        }
    }
}
