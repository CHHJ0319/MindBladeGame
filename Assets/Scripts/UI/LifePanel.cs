using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    public Text PlayerLifeText;
    public Text EscorteeLifeText;

    void Awake()
    {
        UIManager.SetLifePanel(this);
    }
    public void SetPlayerLifetText(string text)
    {
        if (PlayerLifeText != null)
        {
            PlayerLifeText.text = text;
        }
    }

    public void SetEscorteeLifetText(string text)
    {
        if (EscorteeLifeText != null)
        {
            EscorteeLifeText.text = text;
        }
    }
}
