using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private GameState gameState;
    private LevelManager levelManager;
    public string nextLevel;

    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameState.currentLevel = nextLevel;
            levelManager.NextLevel();
        }
    }
}
