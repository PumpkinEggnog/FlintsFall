using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{

    private int maxHealth;
    private int health;

    public HealthSystem(int healthInput)
    {
        this.maxHealth = healthInput;
        this.health = healthInput;
    }

    public void changeHealth(int changeAmount)
    {
        this.health += changeAmount;
        if (this.health > this.maxHealth)
        {
            this.health = this.maxHealth;
        }
    }

    public void setHealth(int changeAmount)
    {
        this.health = changeAmount;
    }

    public int getHealth()
    {
        return this.health;
    }

}
