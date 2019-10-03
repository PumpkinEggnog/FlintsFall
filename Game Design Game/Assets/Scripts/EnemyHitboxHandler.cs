using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxHandler : MonoBehaviour
{
    public HealthSystem health = new HealthSystem(1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.getHealth() <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        HitBox hitbox = other.gameObject.GetComponent<HitBox>();
        this.health.changeHealth(hitbox.value);
        Debug.Log("enemy health "+health.getHealth());
    }
}
