using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string nextLevel;

    
    public void StartGame()
    {
        SceneManager.LoadScene("Rising acid test");
    }

    public void viewCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void exitCredits()
    {
        GameObject.Find("GameState").GetComponent<GameState>().currentLevel = "Tutorial-actual";
        SceneManager.LoadScene("StartScene");
    }

    public void splashAdvance()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void NextLevel()
    {
        nextLevel = GameObject.Find("GameState").GetComponent<GameState>().currentLevel;
        SceneManager.LoadScene(nextLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
