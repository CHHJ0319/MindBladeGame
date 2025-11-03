using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public EscorteeController escorteeController;

    public float moveSpeed = 1f;

    private Rigidbody2D rb;

    private Vector2 targetDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            enabled = false;
            return;
        }
    }

    void FixedUpdate()
    {
        if (!escorteeController.IsMoving)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        SetDirection();
        MovePlayer();
    }

    private void SetDirection()
    {
        targetDir = escorteeController.Direction;
    }

    private void MovePlayer()
    {
        float distanceToMove = moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + targetDir * distanceToMove);

    }

}
