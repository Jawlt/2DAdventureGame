using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public string newGameScene;
    public string leaderboardScene;
    void Start()
    {

    }

    void Update()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene(leaderboardScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
