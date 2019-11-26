﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBounce : MonoBehaviour
{
    private PlayerMove move;

    public AudioSource splat1;

    // Start is called before the first frame update
    void Start()
    {
        move = gameObject.GetComponentInParent<PlayerMove>();

        splat1 = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider Other)
    {
        
        if (Other.gameObject.CompareTag("Enemy"))
        {
            
            move.setJumping(true);
        }
        splat1.Play();

    }

    
}
