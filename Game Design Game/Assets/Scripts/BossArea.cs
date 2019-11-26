using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbasBasement : MonoBehaviour
{
    public BoxCollider territory;
    GameObject player;
    public bool playerInTerritory;
    BossAI bossAI;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerInTerritory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true) // if the player enters the damger zone, get ready to SMASH THE FURRY SHOUTA
        {
            bossAI.LookAtPlayer();
        }

        if (playerInTerritory == false) // if the player is not in the killzone, HENTAI WITH SENPAI
        {
            bossAI.DoNothing();
        }
    }

    void OnTriggerEnter(Collider other) // if the player enters danger zone
    {
        if (other.gameObject.tag == "player")
        {
            playerInTerritory = true; // set bool to true
        }
    }

    void OnTriggerExit(Collider other) // if the player exits the danger zone
    {
        if (other.gameObject.tag == "player")
        {
            playerInTerritory = false; // set bool to false;
        }
    }
}
