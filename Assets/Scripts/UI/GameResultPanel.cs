using UnityEngine;
using UnityEngine.UI;

public class GameResultPanel : MonoBehaviour
{
    public Text gameResultText;

    void Awake()
    {
        UIManager.SetGameResultPanel(this);
    }

    public void SetGameResultText(string text)
    {
        if (gameResultText != null)
        {
            gameResultText.text = text;
        }
    }

    public void HideGameResultPanel()
    {
        gameObject.SetActive(false);
    }
}
