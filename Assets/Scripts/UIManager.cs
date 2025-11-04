using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    private static MessagePanel messagePanel;
    private static GameResultPanel gameResultPanel;
    private static LifePanel lifePanel;

    void Start()
    {
        HideMessagePanel();
        HideGameResultPanel();
    }

    void Update()
    {
        messagePanel.HandleEncouragement();
    }

    public static void SetMessagePanel(MessagePanel panel)
    {
            messagePanel = panel;
    }

    public static void HideMessagePanel()
    {
        if (messagePanel != null)
        {
            messagePanel.HideMessagePanel();
        }
    }

    public static void ResetEncouragementTimer()
    {
        if (messagePanel != null)
        {
            messagePanel.ResetEncouragementTimer();
        }
    }

    public static void SetGameResultPanel(GameResultPanel panel)
    {
        gameResultPanel = panel;
    }

    public static void HideGameResultPanel()
    {
        if (gameResultPanel != null)
        {
            gameResultPanel.HideGameResultPanel();
        }
    }

    public static void ShowGameOverMessage()
    {
        if (gameResultPanel != null)
        {
            gameResultPanel.gameObject.SetActive(true);
            gameResultPanel.SetGameResultText("Game Over");
        }
    }

    public static void ShowGameClearMessage()
    {
        if (gameResultPanel != null)
        {
            gameResultPanel.gameObject.SetActive(true);
            gameResultPanel.SetGameResultText("Game Clear");
        }
    }
    public static void SetLifePanel(LifePanel panel)
    {
        lifePanel = panel;
    }
    public static void UpdatePlayerLifeUI(int lives)
    {
        if (lifePanel == null)
        {
            return;
        }

        if (lives <= 0)
        {
            lifePanel.SetLifetText("Life : 0");
            return;
        }

        string heartIcon = new string('\u2665', Mathf.Clamp(lives, 0, 10));
        lifePanel.SetLifetText($"Life : {lives}  {heartIcon}");
    }
}
