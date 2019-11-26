using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RaiderAI : MonoBehaviour
{

    public Transform player;
    public float range = 100.0f;
    public float bulletImpulse = .005f;
    public float lastShot = -1.0f;
    public float shotDelay = 2.0f;
    public bool Shooting = false;

    private LayerMask mask;

    private bool onRange = false;

    public Rigidbody projectile;

    void Start()
    {
        mask = ~(1 << 12);
        // float rand = Random.Range(1.0f, 2.0f);
        // InvokeRepeating("Shoot", 5, rand);
    }

    public bool Firing()
    {
        return true;
    }
    public void Shoot()
    {

        Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position, transform.rotation);
            // bullet.AddForce(transform.forward * bulletImpulse * Time.deltaTime, ForceMode.Impulse);
        bullet.velocity = (Vector3.Normalize(player.position - transform.position) * 5);
        Debug.Log(Vector3.Normalize(player.position - transform.position));

            // Destroy(bullet.gameObject, 2);

    }

    void OnDrawGizmosSelected() //gives enemy sight a red outline to distinguish area of vision
    {
        Gizmos.color = Color.red; //sets color to red
        Gizmos.DrawWireSphere(transform.position, range); //creates an outline
    }

    void Update()
    {

        onRange = Vector3.Distance(transform.position, player.position) < range;

        RaycastHit hit;
        // Debug.Log(player.position - transform.position);
        if (Physics.Raycast(transform.position, Vector3.Normalize(player.position - transform.position), out hit, range, mask))
        {
            // Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            // Debug.Log(hit.transform.name);
            if ( (hit.transform.tag == "Player") &&
                ((Time.time - lastShot) >= shotDelay) )
            {
                lastShot = Time.time;
                Shoot();
            }
        }

        if (onRange)
        {
            FaceTarget();
        }
            // transform.LookAt(player);
    }

    void FaceTarget() //helps determine where player is in relation to enemy and has the enemy face the player
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion sightRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, sightRotation, Time.deltaTime * 5f);
    }


}
