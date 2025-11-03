using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    public Text lifeText;

    void Start()
    {
        GameManager.SetLifeText(lifeText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
