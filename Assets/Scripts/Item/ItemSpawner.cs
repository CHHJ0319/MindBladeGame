using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Heart Item")]
    [SerializeField] private GameObject heartItemPrefab;
    [SerializeField] private float heartItemSpawnInterval = 15f;
    [SerializeField] private float heartItemLifetime = 6f;

    private float heartItemSpawnTimer;

    private void Awake()
    {
        ActorManager.SetItemSpawner(this);
    }

    public void ResetHeartItemTimer()
    {
        heartItemSpawnTimer = 0f;
    }

    public void HandleHeartItemSpawn()
    {
        if (heartItemPrefab == null)
        {
            return;
        }

        heartItemSpawnTimer += Time.deltaTime;
        if (heartItemSpawnTimer < heartItemSpawnInterval)
        {
            return;
        }
        ResetHeartItemTimer();

        if (TryGetRandomPosition(out Vector2 newPos))
        {
            GameObject item = Instantiate(heartItemPrefab, newPos, Quaternion.identity);
            HeartItem heartItem = item.GetComponent<HeartItem>();
            if (heartItem != null)
            {
                heartItem.Configure(heartItemLifetime);
            }
            else
            {
                Destroy(item, heartItemLifetime);
            }
        }
        else
        {
            return;
        }    
    }

    private bool TryGetRandomPosition(out Vector2 randPos)
    {

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            randPos = Vector2.zero;
            return false;
        }

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;
        randPos = new Vector2(Random.Range(-halfWidth * 0.8f, halfWidth * 0.8f), Random.Range(-halfHeight * 0.8f, halfHeight * 0.8f));

        return true;
    }
}
