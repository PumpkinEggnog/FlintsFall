using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRise : MonoBehaviour
{
    private bool start;
    private float speed = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(start)
        {
            transform.Translate(Vector3.up*speed*Time.deltaTime);            
        }
    }

    public void setStart(bool begin)
    {
        start = begin;
    }
}
