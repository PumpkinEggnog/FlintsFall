using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RaiderAI : MonoBehaviour
{

    public Transform player;
    public float range = 50.0f;
    public float bulletImpulse = 20.0f;
    public bool Shooting = false;

    private bool onRange = false;

    public Rigidbody projectile;

    void Start()
    {
        float rand = Random.Range(1.0f, 2.0f);
        InvokeRepeating("Shoot", 2, rand);
    }

    public bool Firing()
    {
        return true;
    }
    public void Shoot()
    {

        if (onRange)
        {
            Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            bullet.AddForce(transform.forward * bulletImpulse, ForceMode.Impulse);

            Destroy(bullet.gameObject, 2);
        }


    }

    void OnDrawGizmosSelected() //gives enemy sight a red outline to distinguish area of vision
    {
        Gizmos.color = Color.red; //sets color to red
        Gizmos.DrawWireSphere(transform.position, range); //creates an outline
    }

    void Update()
    {

        onRange = Vector3.Distance(transform.position, player.position) < range;

        if (onRange)
            transform.LookAt(player);
    }


}
