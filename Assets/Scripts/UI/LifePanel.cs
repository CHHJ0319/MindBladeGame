using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    public Text lifeText;

    void Awake()
    {
        UIManager.SetLifePanel(this);
    }
    public void SetLifetText(string text)
    {
        if (lifeText != null)
        {
            lifeText.text = text;
        }
    }
}
