using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SwordController : MonoBehaviour
{
    [Tooltip("플레이어 이동 속도")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameRunning)
        {
            movement = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameRunning)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = movement.sqrMagnitude > 1f ? movement.normalized : movement;
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);

        RotateTowardsDirection(dir);
    }

    private void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude == 0)
        {
            return;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;

        transform.rotation = Quaternion.Euler(0, 0, 210f + angle);
    }
}
