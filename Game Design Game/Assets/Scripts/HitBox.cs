using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private BoxCollider collider;
    public float offset = 1f;
    public int value = -1;
    //public bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeDirection(bool facingRight)
    {
        if(facingRight)
        {
            transform.localPosition = Vector3.left * offset;
        }
        else
        {
            transform.localPosition = Vector3.right * offset;
        }
        
    }

    public void attacking(bool isAttacking)
    {
        collider = GetComponent<BoxCollider>();
        collider.enabled = isAttacking;
    }
}
