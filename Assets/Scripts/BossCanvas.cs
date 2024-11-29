using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossCanvas : MonoBehaviour
{
    public static BossCanvas Instance { get; private set; } // Singleton for global access

    // References to TMP texts in the Canvas
    public TextMeshProUGUI userName;
    public TextMeshProUGUI score;
    public LeaderboardManager leaderboardManager;
    private string playerNum;

    private int totalRobotCount;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerNum = Random.Range(0, 10000).ToString("D4");
        userName.text = "";
        score.text = "";
    }

    private void Update()
    {

    }

    public void UpdateUsernameScoreText()
    {
        string username = "Player" + playerNum;
        userName.text = username;
        int time = Mathf.RoundToInt(Timer.Instance.GetFinalTime());
        score.text = "Completed In: " + time.ToString() + "s";
        leaderboardManager.SetLeaderboardEntry(username, time);
    }
}