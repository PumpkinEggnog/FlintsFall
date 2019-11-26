using UnityEngine;

public class BossFightSpawner : MonoBehaviour
{
    public BossHealth bossHealth;
    public GameObject enemy;
    public Transform[] spawnPoints;
    public float health = 5;
    public int spawnPoint;

    void Spawn()
    {
        if (bossHealth.currentHealth == health) // if the boss's health is equal to the inputted health, spawn the raider on the desired spawnpoint
        {
            int spawnPointIndex = spawnPoint;

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        if (bossHealth.isDead == true)
        {
            bossHealth.DestroyGameObject();
        }
    }
}
