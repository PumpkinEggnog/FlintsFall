using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private Door door;

    public AudioClip SwitchClick;

    //private AudioSource SwitchClick;

    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentInParent<Door>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            AudioSource.PlayClipAtPoint(SwitchClick, this.gameObject.transform.position);

            door.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
