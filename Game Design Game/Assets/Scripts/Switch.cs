using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private Door door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentInParent<Door>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
