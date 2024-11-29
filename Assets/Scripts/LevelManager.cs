using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } // Singleton for global access

    public int enemiesRemaining = 6;
    [SerializeField] private PlayerController player;
    public bool levelFinished = false;
    public bool bossDefeated = false;

    // Variables related to Sound
    AudioSource audioSource;
    [SerializeField] AudioClip questComplete;

    private void Awake()
    {
        // Ensure there's only one LevelManager
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }

        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.SetTotalRobotCount(enemiesRemaining);
        }

    }

    public void Update()
    {
        if (bossDefeated && levelFinished)
        {
            if (BossCanvas.Instance != null)
            {
                BossCanvas.Instance.UpdateUsernameScoreText();
            }

            if (menuController.Instance != null)
            {
                menuController.Instance.GameCompleted();
            }
        }
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.UpdateRobotsFixedText(enemiesRemaining);
        }

        if (enemiesRemaining <= 0)
        {
            LevelCompleted();
        }
    }

    public void LevelCompleted()
    {
        if (enemiesRemaining == 0 && player != null && player.hasKeys)
        {
            audioSource.PlayOneShot(questComplete);
            Debug.Log("Level Completed!");
            levelFinished = true;
        }
    }

    public int GetEnemiesRemaining()
    {
        return enemiesRemaining;
    }

    public void BossIsDefeated()
    {
        bossDefeated = true;
        levelFinished = true;
    }
}