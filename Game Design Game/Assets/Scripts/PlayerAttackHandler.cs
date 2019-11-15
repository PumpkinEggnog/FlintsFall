using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private HitBox dashBox;
    private HitBox jumpBox;
    private bool facingRight;
    private bool isAttacking;
    private Animator animator;
    private float attackLength = 0.5f;
    // private float dashLength = 0.003f;
    private float lastAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        jumpBox = GameObject.Find("jumpBox").GetComponent<HitBox>();
        dashBox = GameObject.Find("dashBox").GetComponent<HitBox>();
        animator = GetComponentInChildren<Animator>();
        facingRight = true;
        dashBox.changeDirection(facingRight);
        dashBox.attacking(false);
        isAttacking = false;
    }

    void Update()
    {
        // HitBox hitbox = GetComponentInChildren<HitBox>();
        // if ((Input.GetKeyDown("f") || Input.GetKeyDown("c")) && !isAttacking)
        // {
        //     hitbox.attacking(true);
        //     lastAttackTime = Time.time;
        //     isAttacking = true;
        // }

        animator.SetBool("attacking", isAttacking);
    }


    // Update is called once per frame
    void FixedUpdate()
    {   
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput > 0)
        {
            facingRight = true;
            dashBox.changeDirection(facingRight);
        }
        else if (horizontalInput < 0)
        {
            facingRight = false;
            dashBox.changeDirection(facingRight);
        }

        
        if (Input.GetKeyDown("c") && !isAttacking)
        {
            dashBox.attacking(true);
            lastAttackTime = Time.time;
            isAttacking = true;
        }


        if (isAttacking && ((Time.time - lastAttackTime) >= attackLength))
        {
            dashBox.attacking(false);
            isAttacking = false;
        }
        
        
    }
}
