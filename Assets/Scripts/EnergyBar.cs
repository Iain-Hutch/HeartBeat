using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.Universal;

public class EnergyBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;
    public Image heart;
    public Sprite heart1;
    public Sprite heart2;
    public Sprite heart3;

    public MainScript main_script;

    public UnityEngine.Rendering.Universal.Light2D gb_light;

    public void Start()
    {
        main_script = FindObjectOfType<MainScript>();
    }

    public void Update()
    {
        SetEnergy(main_script.energyBar);
    }

    public void SetMaxEnergy(int energy)
	{
        fill.color = gradient.Evaluate(1f);
        slider.maxValue = energy;
        slider.value = energy;
        gb_light.intensity = 1f;
	}

    public void SetEnergy(int energy)
	{
		slider.value = energy;
		fill.color = gradient.Evaluate(slider.normalizedValue);

        if (slider.value > 80) {heart.sprite = heart1;}
        else{
            gb_light.intensity = (slider.value/100) + 0.2f;
            if (slider.value > 30) {heart.sprite = heart2;}
            else {heart.sprite = heart3;}
        }
        
	}

}