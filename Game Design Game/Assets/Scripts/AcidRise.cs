using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRise : MonoBehaviour
{
    private float speed = .05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
    }
}
