using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0)
        {
            facingRight = true;
        }
        else if (horizontalInput < 0)
        {
            facingRight = false;
        }

        var attack = GetComponentInChildren<HitBox>();
        if (Input.GetKeyDown("v"))
        {
            attack.activate();
        }
        if (Input.GetKeyDown("b"))
        {
            attack.deactivate();
        }
    }
}
