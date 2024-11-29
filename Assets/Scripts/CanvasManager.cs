using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; } // Singleton for global access

    // References to TMP texts in the Canvas
    public TextMeshProUGUI goldenKeysText;
    public TextMeshProUGUI robotsText;

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

        goldenKeysText.text = "";
        robotsText.text = "";
    }

    private void Update()
    {

    }

    public void UpdateGoldenKeysText(int keyCount)
    {
        goldenKeysText.text = "Golden Keys: " + keyCount;
    }

    public void UpdateRobotsFixedText(int robotCount)
    {
        int robotsFixed = totalRobotCount - robotCount;
        robotsText.text = "Robots Fixed: " + robotsFixed + "/" + totalRobotCount;
    }

    public void SetTotalRobotCount(int total)
    {
        totalRobotCount = total;
    }

    public void HideText()
    {
        goldenKeysText.text = "";
        robotsText.text = "";
    }
}
