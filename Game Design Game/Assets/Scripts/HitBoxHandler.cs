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
        HitBox hitbox = other.gameObject.GetComponent<HitBox>();
        this.health.changeHealth(hitbox.value);
        Debug.Log("health "+health.getHealth());    
    }
}
