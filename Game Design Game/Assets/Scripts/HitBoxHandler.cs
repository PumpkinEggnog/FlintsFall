using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBoxHandler : MonoBehaviour
{
    private HealthSystem health = new HealthSystem(3);
    public Text textbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textbox.text = "Health: " + health.getHealth();
    }
    
    void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        if (otherObject.CompareTag("HealthPickup"))
        {
            otherObject.SetActive(false);
            this.health.changeHealth(1);
            Debug.Log("health "+health.getHealth());
        }
        else if (otherObject.CompareTag("EnemyAttack"))
        {
            HitBox hitbox = otherObject.GetComponent<HitBox>();
            if (hitbox.attacking)
            {
                otherObject.SetActive(false);
                this.health.changeHealth(hitbox.damage);
                Debug.Log("health "+health.getHealth());
            }
        }
    }
}
