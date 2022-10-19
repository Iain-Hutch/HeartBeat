using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

	public int maxEnergy = 100;
	public int currentEnergy;
	public EnergyBar energyBar;
	private int boost;
	private int neg_boost;

    // Start is called before the first frame update
    void Start()
    {
		currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(100); 
		resetBoost();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.B))
		{
			addBoost(4);
		}
		else if (Input.GetKeyDown(KeyCode.N))
		{
			addNegativeBoost(4);
		}
		
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			LoseEnergy(5);
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			GainEnergy(5);
		}
    }

	void LoseEnergy(int energy)
	{
		currentEnergy -= (energy * neg_boost);
		energyBar.SetEnergy(currentEnergy);
	}

	void GainEnergy(int energy)
	{
		currentEnergy += (energy * boost);
		energyBar.SetEnergy(currentEnergy);
	}

	void addBoost(int b){
		boost = b;
	}

	void addNegativeBoost(int n){
		neg_boost = n;
	}

	void resetBoost(){
		boost = 1;
		neg_boost = 1;
	}
}