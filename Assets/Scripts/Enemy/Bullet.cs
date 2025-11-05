using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("탄환 이동 속도")]
    public float speed = 10f;

    [Tooltip("탄환 생존 시간 (초)")]
    public float lifeTime = 5f;

    [HideInInspector]
    public Vector2 direction = Vector2.right;

    private void Start()
    {
        RotateTowardsDirection(direction);
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    protected void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude == 0)
        {
            return;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out PlayerController player))
        {
            GameManager.DamagePlayer();
            Destroy(gameObject);
        }
        else if (collider.TryGetComponent(out EscorteeController escortee))
        {
            GameManager.DamageEscortee();
            Destroy(gameObject);
        }
        else if (collider.TryGetComponent(out SwordController sword))
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
        
    }
}
