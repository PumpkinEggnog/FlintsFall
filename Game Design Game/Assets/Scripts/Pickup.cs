using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public AudioClip HealthUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //AudioSource.PlayClipAtPoint(HealthUp, this.gameObject.transform.position);

        this.gameObject.SetActive(false);

        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(HealthUp, this.gameObject.transform.position);
        }
    }
}
