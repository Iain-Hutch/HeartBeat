using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarEffect : MonoBehaviour
{

    private MainScript main_script;

    // Start is called before the first frame update
    void Start()
    {
        main_script = FindObjectOfType<MainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            int rand = Random.Range(0, 301);
            if (main_script.energyBar > rand)
            {
                transform.Translate (1,0,0);
            }
        }
    }
}
