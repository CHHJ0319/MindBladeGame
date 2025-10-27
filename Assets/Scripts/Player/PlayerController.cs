using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Rigidbody2D rb;

    private Vector3 goalPos;
    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            enabled = false;
            return;
        }

        goalPos = SetGoalPosition();
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MovePlayer();
    }

    private Vector3 SetGoalPosition()
    {
        Vector3 viewportCenterLeft = new Vector3(0f, 0.5f, 0f);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportCenterLeft);
        worldPos.z = 0f;

        return worldPos;
    }

    private void MovePlayer()
    {
        Vector2 direction = (goalPos - transform.position).normalized;
        float distanceToMove = moveSpeed * Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, goalPos) <= distanceToMove)
        {
            rb.MovePosition(goalPos);
            isMoving = false;

            OnReachGoal();
        }
        else
        {
            rb.MovePosition(rb.position + direction * distanceToMove);
        }
    }


    private void OnReachGoal()
    {
        Debug.Log("목표 지점에 도착");

        var manager = GameManager.Instance;
        manager.HandleGameClear();
    }


}
