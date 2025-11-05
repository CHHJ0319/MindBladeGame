using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Escortee")]
    public EscorteeController escortee;

    [Header("Weapon")]
    public SwordController sword;

    [Header("Property")]
    public float moveSpeed = 1f;
    [SerializeField] private float invincibilityDuration = 1.5f;

    private float maxEnergy = 100f;
    private float currentEnergy = 100f;
    private float swordMoveEnergyCost = 15f;
    private float restoreEnergyTime = 3f;

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

    private Vector2 targetDir;

    private bool isSwordMovable = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        ActorManager.SetPlayer(this);

        lives = Mathf.Max(1, startingLives);
    }

    void Update()
    {
        UpdateInvincibility();
        MoveSword();
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

    public void InitPlayer()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            enabled = false;
            return;
        }

        isInvincible = false;
        invincibleTimer = 0f;

        if(currentEnergy > 0)
        {
            isSwordMovable = true;
        }
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

    public void AddLife(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        lives += amount;
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
    }

    private void MoveSword()
    {
        if (GameManager.IsGameRunning && sword != null)
        {
            if (!isSwordMovable)
            {
                sword.Move(0f, 0f);
                return;
            }

            float movemontX = Input.GetAxisRaw("Horizontal");
            float movemontY = Input.GetAxisRaw("Vertical");
            sword.Move(movemontX, movemontY);


            if (movemontX != 0 || movemontY != 0)
            {
                currentEnergy -= swordMoveEnergyCost * Time.deltaTime;

                float energyRatio= 0f;
                if (currentEnergy <= 0)
                {
                    isSwordMovable = false;
                    energyRatio = 0f;

                    escortee.IsMoving = false;

                    StopAllCoroutines();
                    StartCoroutine(RestoreEnergy());
                }
                else
                {
                    energyRatio = currentEnergy / maxEnergy;
                }
                UIManager.UpdateEnergybarUI(energyRatio);
            }
        }
    }

    private IEnumerator RestoreEnergy()
    {
        float elapsedTime = 0f;

        while (elapsedTime < restoreEnergyTime)
        {
            float fillRatio = elapsedTime / restoreEnergyTime;

            currentEnergy = Mathf.Lerp(0f, maxEnergy, fillRatio);

            UIManager.UpdateEnergybarUI(fillRatio);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        currentEnergy = maxEnergy;
        UIManager.UpdateEnergybarUI(1f);

        isSwordMovable = true;
        escortee.IsMoving = true;
    }
}
