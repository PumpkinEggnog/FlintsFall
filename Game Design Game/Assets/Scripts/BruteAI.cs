using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BruteAI : MonoBehaviour
{
    public float sightRadius = 5f;
    private bool isAttacking;
    private float attackLength = 1f;
    private float lastAttack = -1f;
    private float attackDelay = 1f;
    Transform target;
    NavMeshAgent agent;
    public GameObject playerObj;

    //initialization
    void Start()
    {
        HitBox hitbox = GetComponentInChildren<HitBox>();
        isAttacking = false;
        target = AISightManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        HitBox hitbox = GetComponentInChildren<HitBox>();
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= sightRadius) //if the player moves into line of sight
        {
            agent.SetDestination(target.position); //move towards the player
            if (distance <= agent.stoppingDistance) //checks to see if the mutant is next to the player
            {
                FaceTarget(); //causes AI to face player
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
        transform.LookAt(playerObj.transform.position);
    }
}
