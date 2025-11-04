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
    [Header("플레이어 설정")]
    [Tooltip("게임에서 조작할 플레이어 스크립트")]
    [SerializeField] private SwordController sword;

    [Tooltip("격려 메시지를 표시할 Text")]
    [SerializeField] private Text messageText;

    [Tooltip("게임 오버 시 점수를 표시하는 Text")]
    [SerializeField] private Text gameOverScoreText;

    [Header("하트 아이템")]
    [Tooltip("생명을 회복하는 하트 아이템 프리팹")]
    [SerializeField] private GameObject heartItemPrefab;

    [Tooltip("하트 아이템이 등장하는 간격(초)")]
    [SerializeField] private float heartSpawnInterval = 15f;

    [Tooltip("하트 아이템이 사라지기까지 유지되는 시간(초)")]
    [SerializeField] private float heartLifetime = 6f;

    private static PlayerController player;
    private static BulletSpawner bulletSpawner;  

    private static float elapsedTime;
    public static float ElapsedTime => elapsedTime;


    // 하트 아이템 생성 타이머
    private float heartTimer;

    private static bool isGameOver;
    public static bool IsGameRunning => !isGameOver;


    private void Awake()
    {
        if (sword == null)
        {
            sword = FindObjectOfType<SwordController>();
        }
    }

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

        
        HandleHeartSpawn();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        elapsedTime = 0f;
        
        heartTimer = 0f;

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

    /// <summary>
    /// 일정 시간마다 하트 아이템을 화면 내 랜덤 위치에 생성
    /// </summary>
    private void HandleHeartSpawn()
    {
        if (heartItemPrefab == null)
        {
            return;
        }

        heartTimer += Time.deltaTime;
        // 하트 생성 간격이 안 됐으면 리턴
        if (heartTimer < heartSpawnInterval)
        {
            return;
        }

        heartTimer = 0f;

        Camera cam = Camera.main;
        if (cam == null)
        {
            return;
        }

        // 화면 내 랜덤 위치 계산
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        Vector2 randomPosition = new Vector2(Random.Range(-halfWidth * 0.8f, halfWidth * 0.8f), Random.Range(-halfHeight * 0.8f, halfHeight * 0.8f));

        // 하트 아이템 생성
        GameObject heart = Instantiate(heartItemPrefab, randomPosition, Quaternion.identity);
        HeartItem item = heart.GetComponent<HeartItem>();
        if (item != null)
        {
            // 하트 아이템에 유지 시간 설정
            item.Configure(heartLifetime);
        }
        else
        {
            // HeartItem 컴포넌트가 없으면 일정 시간 후 자동 삭제
            Destroy(heart, heartLifetime);
        }
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
