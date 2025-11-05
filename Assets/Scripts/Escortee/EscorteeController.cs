using UnityEngine;

public class EscorteeController : MonoBehaviour
{
    [Header("Property")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float invincibilityDuration = 1.5f;

    private Vector3 goalPos;
    private Vector2 direction;
    public Vector2 Direction
    {
        get { return direction; }
    }

    private int lives;
    public int Lives
    {
        get { return lives; }
    }
    private int startingLives = 1;

    private bool isInvincible;
    public bool IsInvincible
    {
        get { return isInvincible; }
    }
    private float invincibleTimer;

    private bool isMoving = true;
    public bool IsMoving
    {
        get { return isMoving; }
    }

    private Rigidbody2D rb;


    private void Awake()
    {
        ActorManager.SetEscortee(this);
        lives = Mathf.Max(1, startingLives);
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

    void Update()
    {
        UpdateInvincibility();
    }

    public void InitEscortee()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            enabled = false;
            return;
        }

        goalPos = SetGoalPosition();

        isInvincible = false;
        invincibleTimer = 0f;
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

        GameManager.HandleGameClear();
    }

    public void TakeDamage()
    {
        lives = Mathf.Max(0, lives - 1);
    }

    public void AddLife(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        lives += amount;
    }
    private void UpdateInvincibility()
    {
        if (!isInvincible)
        {
            return;
        }

        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer <= 0f)
        {
            isInvincible = false;
        }
    }

    public void StartInvincibility()
    {
        isInvincible = true;
        invincibleTimer = invincibilityDuration;
    }
}
