using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteStats : MonoBehaviour
{
    public int maxHealth = 1000000; //not dying today lol
    public int currentHealth { get; private set; } //used to keep track of how much health is left

    void Awake()
    {
        currentHealth = maxHealth; //spawn enemy with max health
    }

    public virtual void Die() //if we manage to kill the mutant
    {
        Debug.Log("Brute has been defeated."); //add death to log for development purposes
    }
}
