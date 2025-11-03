using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    public Text messageText;
    void Start()
    {
        GameManager.SetMessagePanel(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
