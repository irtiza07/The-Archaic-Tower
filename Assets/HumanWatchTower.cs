using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWatchTower : MonoBehaviour {

    public int currentHealth;
    public int maximumHealth;

    public double upgradeHealthMultiplier;
    public int repairHealthIncrement;
	// Use this for initialization
	void Start () {
        maximumHealth = 10000;
        currentHealth = maximumHealth;
        upgradeHealthMultiplier = 1.5;
        repairHealthIncrement = 50;
	}

    private void OnTriggerStay(Collider other)
    {
        currentHealth -= 1;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
