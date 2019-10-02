﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantAI : MonoBehaviour
{
    public float sightRadius = 5f;

    Transform target;
    NavMeshAgent agent;

    //initialization
    void Start()
    {
        target = AISightManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= sightRadius) //if the player moves into line of sight
        {
            agent.SetDestination(target.position); //move towards the player

            if(distance <= agent.stoppingDistance) //checks to see if the mutant is next to the player
            {
                FaceTarget(); //causes AI to face player
                //insert attack here
            }
        }
    }

    void OnDrawGizmosSelected() //gives enemy sight a red outline to distinguish area of vision
    {
        Gizmos.color = Color.red; //sets color to red
        Gizmos.DrawWireSphere(transform.position, sightRadius); //creates an outline
    }

    void FaceTarget() //helps determine where player is in relation to enemy and has the enemy face the player
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion sightRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, sightRotation, Time.deltaTime * 5f);
    }
}
