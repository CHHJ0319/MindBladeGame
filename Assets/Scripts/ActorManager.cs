using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static ItemSpawner itemSpawner;
    private static BulletSpawner bulletSpawner;

    void Update()
    {
        HandleHeartItemSpawn();
    }

    public static void SetItemSpawner(ItemSpawner spawner)
    {
        itemSpawner = spawner;
    }

    public static void ResetHeartItemSpawnTimer()
    {
        if(itemSpawner != null)
        {
            itemSpawner.ResetHeartItemTimer();
        }
    }

    private static void HandleHeartItemSpawn()
    {
        if (itemSpawner != null)
        {
            itemSpawner.HandleHeartItemSpawn();
        }
    }

    public static void SetBulletSpawner(BulletSpawner spawner)
    {
        bulletSpawner = spawner;
    }

    public static void EnableBulletSpawner()
    {
        if (bulletSpawner != null)
        {
            bulletSpawner.enabled = true;
        }
    }

    public static void DisableBulletSpawner()
    {
        if (bulletSpawner != null)
        {
            bulletSpawner.enabled = false;
        }
    }
}
