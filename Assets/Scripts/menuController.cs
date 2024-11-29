using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public static menuController Instance { get; private set; }
    public string mainMenuScene;
    public string mainLevel;
    public GameObject pauseMenu;
    public GameObject restartMenu;
    public GameObject gameCompleted;
    public bool isPaused;

    private PlayerController playerController;

    void Awake()
    {
        // Ensure there's only one LevelManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isPaused = false;
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (playerController != null)
        {
            playerController.isPaused = true;
        }
    }

    public void RestartMenu()
    {
        isPaused = true;
        restartMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void GameCompleted()
    {
        isPaused = true;
        gameCompleted.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (playerController != null)
        {
            playerController.isPaused = false;
        }
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;


        if (Timer.Instance != null)
        {
            Timer.Instance.DestroyTimer();
        }
        SceneManager.LoadScene(mainMenuScene);
    }

    public void RestartGame()
    {
        // Reset time scale in case the game was paused
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
