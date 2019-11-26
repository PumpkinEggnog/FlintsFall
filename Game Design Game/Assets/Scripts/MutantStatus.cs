using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantStatus : MutantStats
{
    //
    public AudioSource splat1;
    //

    void Start()
    {
        splat1 = GetComponent<AudioSource>();
    }
    public override void Die() //if we kill the mutant
    {

        splat1.Play();
        base.Die(); //it dies in log to keep track of player kills
        //add death animation or nah?
        Destroy(gameObject); //delete from screen
    }
}

