using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTrigger : MonoBehaviour
{
    AcidRise acid;
    // Start is called before the first frame update
    void Start()
    {
        acid = GetComponentInParent<AcidRise>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            acid.setStart(true);
            this.gameObject.SetActive(false);
        }
    }
}
