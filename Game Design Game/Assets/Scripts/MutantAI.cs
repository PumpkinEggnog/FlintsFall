using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantAI : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidBody;

    public float sightRadius = 5f;

    private bool isAttacking;
    private bool isWalking;
    // private float attackLength = 1f;
    // private float lastAttack = -1f;
    // private float attackDelay = 1f;

    Transform target;
    NavMeshAgent agent;

    //initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        HitBox hitbox = GetComponentInChildren<HitBox>();
        isWalking = false;
        isAttacking = false;
        target = AISightManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Debug.Log(transform.velocity);

        // animator.SetBool("attacking", isAttacking);
        // animator.SetBool("walking", isWalking);
    }

    void FixedUpdate()
    {
        HitBox hitbox = GetComponentInChildren<HitBox>();
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= sightRadius) //if the player moves into line of sight
        {
            animator.SetFloat("Blend", 1);
            agent.SetDestination(target.position); //move towards the player
            isWalking = true;

            if(distance <= agent.stoppingDistance) //checks to see if the mutant is next to the player
            {
                FaceTarget(); //causes AI to face player
                isWalking = false;
                isAttacking = true;
                animator.SetFloat("Blend", 0);
            }
            else
            {
                isAttacking = false;
            }
        }
        else
        {
            isWalking = false;
            animator.SetFloat("Blend", 0);
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
