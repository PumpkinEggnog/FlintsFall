using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantStats : MonoBehaviour
{

    
    
    public int maxHealth = 10; //default 10 hp for testing purposes
    public int currentHealth { get; private set; } //used to keep track of how much health is left

    void Awake()
    {
        

        currentHealth = maxHealth; //spawn enemy with max health
    }

    public void TakeDamage (int damage) //if the mutant takes damage
    {
        currentHealth -= damage; //subtract the players damage from mutants current health
        Debug.Log("Mutant takes " + damage + " damage."); //add damage to log for development purposes

        if (currentHealth <= 0) //check to see if the mutant has less than or equal to 0 health
        {
            
            Die(); //if it does then run Die function in MutantStatus
        }
    }

    public virtual void Die() //if we manage to kill the mutant
    {
        Debug.Log("Mutant has been defeated."); //add death to log for development purposes
    }
}
