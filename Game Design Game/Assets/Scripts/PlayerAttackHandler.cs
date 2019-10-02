using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private bool facingRight;
    private bool isAttacking;
    private float attackLength = 1f;
    private float lastAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        HitBox hitbox = GetComponentInChildren<HitBox>();
        facingRight = true;
        hitbox.changeDirection(facingRight);
        hitbox.attacking(false);
        isAttacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HitBox hitbox = GetComponentInChildren<HitBox>();
        
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput > 0)
        {
            facingRight = true;
            hitbox.changeDirection(facingRight);
        }
        else if (horizontalInput < 0)
        {
            facingRight = false;
            hitbox.changeDirection(facingRight);
        }

        if (Input.GetKeyDown("f") && !isAttacking)
        {
            hitbox.attacking(true);
            lastAttackTime = Time.time;
            isAttacking = true;
        }

        if (isAttacking && ((Time.time - lastAttackTime) >= attackLength))
        {
            hitbox.attacking(false);
            isAttacking = false;
        }
        
        
    }
}
