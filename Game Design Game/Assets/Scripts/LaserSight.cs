using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    //public Vector3 sphereRadius;
    LineRenderer line;
    RaiderAI raiderAI;
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        raiderAI = GetComponentInParent<RaiderAI>();
    }

    void FixedUpdate()
    {
        line.enabled = true;
        Ray ray = new Ray(transform.position, raiderAI.playerVector);
        RaycastHit hit;

        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, 100))
            line.SetPosition(1, hit.point);
        else
            line.SetPosition(1, ray.GetPoint(100));
    }

}
