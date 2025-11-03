using UnityEngine;
using UnityEngine.UI;

public class GameResultPanel : MonoBehaviour
{
    public Text gameResultText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.SetGameResultPanel(this);
    }

    public void SetGameResultText(string text)
    {
        if (gameResultText != null)
        {
            gameResultText.text = text;
        }
    }
}
