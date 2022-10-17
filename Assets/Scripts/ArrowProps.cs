using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProps : MonoBehaviour
{
    private MainScript main_script;

    public float duration;
    public string type;
    public float time;

    public bool stopping = false;
    public float stopTime;

    // Start is called before the first frame update
    void Start()
    {
        main_script = FindObjectOfType<MainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!main_script.gameOver && !stopping)
        {
            transform.Translate(0, 1.2f * Time.deltaTime, 0);
        }
        if ((main_script.incTime - time > 5) && Equals(type, "m"))
        {
            stopping = true;
            stopTime = main_script.incTime;
        }
        if ((main_script.incTime - stopTime) > duration * 0.05)
        {
            stopping = false;
        }
    }
}
