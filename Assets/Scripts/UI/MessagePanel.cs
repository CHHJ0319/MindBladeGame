using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    public Text messageText;

    [SerializeField] private float encouragementDuration = 2.5f;

    private float encouragementInterval = 10f;
    private static float encouragementTimer;

    [Header("격려 메시지 설정")]
    [SerializeField] private string[] encouragementMessages =
    {
            "좋아요! 계속 버텨봐요!",
            "집중력을 유지하세요!",
            "조금만 더!",
            "리듬을 잃지 마세요!"
    };

    void Start()
    {
        UIManager.SetMessagePanel(this);

        encouragementTimer = 0f;
    }

    void Update()
    {
        HandleEncouragement();
    }

    private void HandleEncouragement()
    {
        if (GameManager.ElapsedTime >= encouragementInterval)
        {
            ShowEncouragement();
            encouragementInterval += 10f;
        }

        if (encouragementTimer > 0f)
        {
            encouragementTimer -= Time.deltaTime;
            if (encouragementTimer <= 0f)
            {
                HideMessagePanel();
            }
        }
    }

    private void ShowEncouragement()
    {
        if (messageText == null || encouragementMessages == null || encouragementMessages.Length == 0)
        {
            return;
        }

        var message = encouragementMessages[Random.Range(0, encouragementMessages.Length)];
        messageText.text = message;
        gameObject.SetActive(true);
        encouragementTimer = encouragementDuration;
    }

    public void HideMessagePanel()
    {
        gameObject.SetActive(false);
    }

    public void ResetEncouragementTimer()
    {
        encouragementTimer = 0f;
    }
}
