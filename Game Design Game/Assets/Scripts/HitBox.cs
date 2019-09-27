using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int damage = -1;
    public bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {
        attacking = true;
    }

    public void deactivate()
    {
        attacking = false;
    }
}
