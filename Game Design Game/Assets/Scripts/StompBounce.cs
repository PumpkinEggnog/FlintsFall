using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBounce : MonoBehaviour
{
    private PlayerMove move;

    // Start is called before the first frame update
    void Start()
    {
        move = gameObject.GetComponentInParent<PlayerMove>();
    }

    public void OnTriggerEnter(Collider Other)
    {
        
        if (Other.gameObject.CompareTag("Enemy"))
        {
            move.setJumping(true);
        }

    }

    
}
