using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
    public bool damageTaken;
    public bool isDead;

    public void TakingDamage(int damageAmount, Vector3 hitPoint) // if boss takes damage, take away appropriate amount of hp an
    {
        damageTaken = true;
        currentHealth -= damageAmount;
        if (currentHealth <= 0) // if the boss is dead
        {
            Dead();
        }
    }

    public BossHealth(int healthInput)
    {
        this.startingHealth = healthInput;
        this.currentHealth = healthInput;
    }

    public void changeHealth(int changeAmount)
    {
        this.currentHealth += changeAmount;
        if (this.currentHealth > this.startingHealth)
        {
            this.currentHealth = this.startingHealth;
        }
        if (this.currentHealth <= 0)
        {
            Dead();
        }
    }

    public void setHealth(int changeAmount)
    {
        this.currentHealth = changeAmount;
    }

    public int getHealth()
    {
        return this.currentHealth;
    }

    void Dead() // set boss to dead, play potential death animation, and possibly make flint do the macarena
    {
        isDead = true;
        // interrupt scene and show close up of boss
        // insert boss death animation
        ExecuteAfterDeath(7);
        // insert flint macarena victory dance
        // roll credits
    }

    IEnumerator ExecuteAfterDeath(float time) // let death animation play out 
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        DestroyGameObject();
        
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
