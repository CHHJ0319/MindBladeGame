using UnityEngine;

public class GuidedBullet : Bullet
{
    public GameObject target;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("GuidedBullet requires a Rigidbody2D component on the GameObject.");
        }
    }

    private void Start()
    {
        if (target == null)
        {
            return;
        }

        direction = (target.transform.position - transform.position).normalized;
        RotateTowardsDirection(direction);
        
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        rb.linearVelocity = transform.up * speed;
    }

}
