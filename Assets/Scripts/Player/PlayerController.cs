using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Escortee")]
    public EscorteeController escortee;

    [Header("Property")]
    public float moveSpeed = 1f;
    [SerializeField] private float invincibilityDuration = 1.5f;

    private bool isInvincible;
    public bool IsInvincible 
    {  
        get { return isInvincible; }
    }
    private float invincibleTimer;

    private int lives;
    public int Lives 
    { 
        get { return lives; }
    }
    private int startingLives = 3;

    private Rigidbody2D rb;

    private Vector2 targetDir;

    private void Awake()
    {
        GameManager.SetPlayer(this);

        lives = Mathf.Max(1, startingLives);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            enabled = false;
            return;
        }

        isInvincible = false;
        invincibleTimer = 0f;      
    }

    void Update()
    {
        UpdateInvincibility();
    }

    void FixedUpdate()
    {
        if (!escortee.IsMoving)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        SetDirection();
        MovePlayer();
    }

    private void SetDirection()
    {
        targetDir = escortee.Direction;
    }

    private void MovePlayer()
    {
        float distanceToMove = moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + targetDir * distanceToMove);

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

    public void TakeDamage()
    {
        lives = Mathf.Max(0, lives - 1);
    }

    public void AddLife(int amount = 1)
    {
        if (amount <= 0)
        {
            return;
        }

        lives += amount;
    }
}
