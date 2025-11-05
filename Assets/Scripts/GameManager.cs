using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        InitGame();

        ActorManager.ResetHeartItemSpawnTimer();
        UpdatePlayerLifeUI();
        UpdateEscorteeLifeUI();
        ActorManager.EnableBulletSpawner();
    }

    private void InitGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        elapsedTime = 0f;
    }

    public static void DamagePlayer()
    {
        if (!IsGameRunning)
        {
            return;
        }

        ActorManager.DamagePlayer(out isGameOver);
        UpdatePlayerLifeUI();

        if (isGameOver)
        {
            HandleGameOver();
        }
    }

    public static void AddPlayerLife(int amount = 1)
    {
        ActorManager.AddLife(amount);
        UpdatePlayerLifeUI();
    }

    public static void DamageEscortee()
    {
        if (!IsGameRunning)
        {
            return;
        }

        ActorManager.DamageEscortee(out isGameOver);
        UpdateEscorteeLifeUI();

        if (isGameOver)
        {
            HandleGameOver();
        }
    }

    private static void HandleGameOver()
    {
        Time.timeScale = 0f;

        ActorManager.DisableBulletSpawner();

        UIManager.HideMessagePanel();
        UIManager.ResetEncouragementTimer();
        UIManager.ShowGameOverMessage();   
    }

    public static void HandleGameClear()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        ActorManager.DisableBulletSpawner();

        UIManager.HideMessagePanel();
        UIManager.ResetEncouragementTimer();
        UIManager.ShowGameClearMessage();
    }

    private void RestartGame()
    {
        InitGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private static void UpdatePlayerLifeUI()
    {
        int playerLives = ActorManager.GetPlayerLives();
        UIManager.UpdatePlayerLifeUI(playerLives);
    }

    private static void UpdateEscorteeLifeUI()
    {
        int escorteeLives = ActorManager.GetEscorteeLives();
        UIManager.UpdateEscorteeLifeUI(escorteeLives);
    }
}
