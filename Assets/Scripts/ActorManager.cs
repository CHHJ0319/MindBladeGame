using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static ItemSpawner itemSpawner;

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

    private void HandleHeartItemSpawn()
    {
        if (itemSpawner != null)
        {
            itemSpawner.HandleHeartItemSpawn();
        }
    }
}
