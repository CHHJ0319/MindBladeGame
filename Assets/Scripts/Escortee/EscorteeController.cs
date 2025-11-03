using UnityEngine;

public class EscorteeController : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Vector3 goalPos;
    private Vector2 direction;
    public Vector2 Direction
    {
        get { return direction; }
    }

    private Rigidbody2D rb;

    private bool isMoving = true;
    public bool IsMoving
    {
        get { return isMoving; }
    }

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

        MoveToGoal();
    }

    private Vector3 SetGoalPosition()
    {
        Vector3 viewportCenterLeft = new Vector3(0f, 0f, 0f);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportCenterLeft);
        worldPos.y = transform.position.y;
        worldPos.z = 0f;

        return worldPos;
    }

    private void MoveToGoal()
    {
         direction = (goalPos - transform.position).normalized;
        float distanceToMove = moveSpeed * Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, goalPos) <= distanceToMove)
        {
            rb.MovePosition(goalPos);
            isMoving = false;

            OnGoalReached();
        }
        else
        {
            rb.MovePosition(rb.position + direction * distanceToMove);
        }
    }

    private void OnGoalReached()
    {
        Debug.Log("목표 지점에 도착");

        var manager = GameManager.Instance;
        manager.HandleGameClear();
    }
}
