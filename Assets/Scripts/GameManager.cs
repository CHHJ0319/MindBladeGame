using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 게임의 전체 상태를 관리하는 매니저 클래스입니다.
/// - 플레이어 생명, 무적, UI, 아이템, 랭킹, 게임 오버 등 모든 게임 흐름을 제어합니다.
/// </summary>
public class GameManager : MonoBehaviour
{
    private static PlayerController player;
    private static BulletSpawner bulletSpawner;  

    private static float elapsedTime;
    public static float ElapsedTime => elapsedTime;

    private static bool isGameOver;
    public static bool IsGameRunning => !isGameOver;


    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!IsGameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                RestartGame();
            }
            return;
        }

        elapsedTime += Time.deltaTime;
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        elapsedTime = 0f;

        ActorManager.ResetHeartItemSpawnTimer();

        UIManager.UpdatePlayerLifeUI(player.Lives);

        if (bulletSpawner != null)
        {
            bulletSpawner.enabled = true;
        }
    }

    public static void DamagePlayer()
    {
        if (!IsGameRunning || player.IsInvincible)
        {
            return;
        }

        player.TakeDamage();
        UIManager.UpdatePlayerLifeUI(player.Lives);

        // 생명이 0 이하이면 게임 오버 처리
        if (player.Lives <= 0)
        {
            HandleGameOver();
        }
        else
        {
            player.StartInvincibility();
        }
    }

    private static void HandleGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (bulletSpawner != null)
        {
            bulletSpawner.enabled = false;
        }

        UIManager.HideMessagePanel();
        UIManager.ResetEncouragementTimer();
        UIManager.ShowGameOverMessage();   
    }

    public static void HandleGameClear()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (bulletSpawner != null)
        {
            bulletSpawner.enabled = false;
        }

        UIManager.HideMessagePanel();
        UIManager.ResetEncouragementTimer();
        UIManager.ShowGameClearMessage();
    }

    public static void AddLife(int amount = 1)
    {
        player.AddLife();
        UIManager.UpdatePlayerLifeUI(player.Lives);
    }

    /// <summary>
    /// 현재 씬을 다시 로드하여 게임 재시작
    /// </summary>
    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void SetBulletSpawner(BulletSpawner spawner)
    {
        bulletSpawner = spawner;
    }

    public static void SetPlayer(PlayerController playerController)
    {
        player = playerController;
    }
}
