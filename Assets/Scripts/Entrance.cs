using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public string nextStageScene;
    private PlayerController playerController;
    public bool firstStageEntrance;
    public bool secondStageEntrance;
    public bool secondStageExit;
    public bool puzzleStageExit;
    public bool isBlocked;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Call EnemyDefeated on the LevelManager singleton
        if (LevelManager.Instance.levelFinished && !isBlocked)
        {
            if (CanvasManager.Instance != null)
            {
                CanvasManager.Instance.HideText();
            }
            UIHandler.Instance.UIHandlerDestroy();
            SceneManager.LoadScene(nextStageScene);
        }
        else
        {
            if (firstStageEntrance && UIHandler.Instance != null)
            {
                UIHandler.Instance.EntranceDisplayDialogue();

            }
            if (secondStageEntrance && UIHandler.Instance != null)
            {
                UIHandler.Instance.SecondStageEntranceDisplay();

            }
            if (secondStageExit && UIHandler.Instance != null)
            {
                UIHandler.Instance.SecondStageExitEntranceDisplay();

            }
        }
    }
}