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
        if (GameManager.Instance != null && !GameManager.Instance.IsGameRunning)
        {
            return;
        }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void RotateTowardsDirection(Vector3 direction)
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
        var manager = GameManager.Instance;
        if (manager == null || !manager.IsGameRunning)
        {
            return;
        }

        if (collider.TryGetComponent(out PlayerController player))
        {
            manager.DamagePlayer();
            Destroy(gameObject);
        }
        else if (collider.TryGetComponent(out SwordMove sword))
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
        
    }
}
