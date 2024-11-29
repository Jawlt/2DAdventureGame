using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    private VisualElement m_Healthbar, m_GoldenKey;
    public static UIHandler Instance { get; private set; }

    // UI dialogue window variables
    public float displayTime = 4.0f;
    public float popUpTime = 2.0f;
    private VisualElement m_NonPlayerDialogue;
    private VisualElement m_EntranceDialogue;
    private VisualElement m_SecondEntranceDialogue;
    private VisualElement m_SecondExitDialogue;
    private float m_NPCTimerDisplay, m_EntranceTimerDisplay, m_SecondEntranceTimer, m_SecondExitTimer;

    // Awake is called when the script instance is being loaded (in this situation, when the game scene loads)
    private void Awake()
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

    // Start is called before the first frame update
    private void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_GoldenKey = uiDocument.rootVisualElement.Q<VisualElement>("GoldenKey");
        SetGoldenKeyVisible(false);

        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_NPCTimerDisplay = -1.0f;

        m_EntranceDialogue = uiDocument.rootVisualElement.Q<VisualElement>("EntranceDialogue");
        m_EntranceDialogue.style.display = DisplayStyle.None;
        m_EntranceTimerDisplay = -1.0f;

        m_SecondEntranceDialogue = uiDocument.rootVisualElement.Q<VisualElement>("SecondStandEntranceDialogue");
        m_SecondEntranceDialogue.style.display = DisplayStyle.None;
        m_SecondEntranceTimer = -1.0f;

        m_SecondExitDialogue = uiDocument.rootVisualElement.Q<VisualElement>("SecondStageExitEntranceDialogue");
        m_SecondExitDialogue.style.display = DisplayStyle.None;
        m_SecondExitTimer = -1.0f;
    }

    private void Update()
    {
        if (m_NPCTimerDisplay > 0)
        {
            m_NPCTimerDisplay -= Time.deltaTime;
            if (m_NPCTimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }

        if (m_EntranceTimerDisplay > 0)
        {
            m_EntranceTimerDisplay -= Time.deltaTime;
            if (m_EntranceTimerDisplay < 0)
            {
                m_EntranceDialogue.style.display = DisplayStyle.None;
            }
        }

        if (m_SecondEntranceTimer > 0)
        {
            m_SecondEntranceTimer -= Time.deltaTime;
            if (m_SecondEntranceTimer < 0)
            {
                m_SecondEntranceDialogue.style.display = DisplayStyle.None;
            }
        }

        if (m_SecondExitTimer > 0)
        {
            m_SecondExitTimer -= Time.deltaTime;
            if (m_SecondExitTimer < 0)
            {
                m_SecondExitDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    public void SetGoldenKeyVisible(bool visible)
    {
        if (visible)
        {
            m_GoldenKey.style.display = DisplayStyle.Flex;
        }
        else
        {
            m_GoldenKey.style.display = DisplayStyle.None;
        }
    }

    public void NPCDisplayDialogue()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_NPCTimerDisplay = displayTime;
    }

    public void EntranceDisplayDialogue()
    {
        m_EntranceDialogue.style.display = DisplayStyle.Flex;
        m_EntranceTimerDisplay = popUpTime;
    }

    public void SecondStageEntranceDisplay()
    {
        m_SecondEntranceDialogue.style.display = DisplayStyle.Flex;
        m_SecondEntranceTimer = popUpTime;
    }

    public void SecondStageExitEntranceDisplay()
    {
        m_SecondExitDialogue.style.display = DisplayStyle.Flex;
        m_SecondExitTimer = popUpTime;
    }

    public void UIHandlerDestroy()
    {
        Destroy(gameObject);
    }
}
