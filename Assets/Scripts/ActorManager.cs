using UnityEditor;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static PlayerController player;

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
    public static void SetPlayer(PlayerController playerController)
    {
        player = playerController;
    }

    public static void DamagePlayer(out bool isGameover)
    {
        isGameover = false;

        if (player != null)
        {
            if (player.IsInvincible)
            {
                return;
            }

            player.TakeDamage();

            if (player.Lives <= 0)
            {
                isGameover = true;
            }
            else
            {
                player.StartInvincibility();
            }
        }
    }

    public static void AddLife(int amount)
    {
        player.AddLife(amount);
    }

    public static int GetPlayerLives()
    {
        if (player != null)
        {
            return player.Lives;
        }
        else
        {
            return 0;
        }
    }
}
