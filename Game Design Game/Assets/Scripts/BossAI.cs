using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Vector3 direction;
    public Transform target;
    public bool isIdle;
    public bool isSmashing;
    public bool isStuck;
    public bool isAttacking;
    public float Sm = 2.0f; // time for Smashing
    public float I = 2.0f; // time for Idle
    public float St = 6.0f; // time for Stuck
    private bool isInvincible;
    private float iFrameLength;
    private float iFrameStart;
    BossHealth bossHealth;
    void Start() // at the start of the boss fight, chill for a sec
    {
        DoNothing();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking == true) // if the boss is looking at the player, attack animation
        {
            AttackPlayer();
        }
        else
            DoNothing();
    }

    public void LookAtPlayer() // look at the player if they enter "attacking territory"
    {
        isAttacking = true;
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
    }
    public void DoNothing() // if the player in not in the "attacking territory", idle animation
    {
        isAttacking = false;
        isStuck = false;
        isSmashing = false;
        isIdle = true;
    }
    public void AttackPlayer() // go through attack phases, GO THWOMP GO!
    {
        StartCoroutine(ExecuteAfterIdle(I));
        StartCoroutine(ExecuteAfterSmash(Sm));
        while (isStuck == true)
        {
            StartCoroutine(ExecuteAfterStuck(St));
            if (bossHealth.damageTaken == true) // if damage is taken, the boss returns to idle state
            {
                StopCoroutine(ExecuteAfterStuck(St));
                StartCoroutine(ExecuteAfterStuck(0.0f));
            }
        }       
    }

    IEnumerator ExecuteAfterIdle(float time) // animation back to idle takes 2 - 3 seconds
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        isSmashing = true;
        isIdle = false;
        isStuck = false;
        if (isInvincible && ((Time.time - iFrameStart) >= iFrameLength))
        {
            setInvincible(false);
        }

    }
    IEnumerator ExecuteAfterSmash(float time) // animation to smash head against floor takes 2 - 3 seconds
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        isSmashing = false;
        isIdle = false;
        isStuck = true;
    }
    IEnumerator ExecuteAfterStuck(float time) // head stuck in lowest attack animation state about 6 seconds
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        isSmashing = false;
        isIdle = true;
        isStuck = false;
        isInvincible = true;
    }
    public void setInvincible(bool input, float duration = 3) // duration = amount time of invincibility frames are up
    {
        isInvincible = input;
        iFrameLength = duration;
        iFrameStart = Time.time;
        Debug.Log("invincibility is set to " + isInvincible);
    }
}
