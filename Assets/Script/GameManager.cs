using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To restart the game

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float timeRemaining = 180f; // 3 minutes (180 seconds)
    private bool isGameOver = false;
    public Text timerText;  // Assign in Inspector (Timer UI)
    public Text gameOverText;  // Assign in Inspector (Win/Lose UI)

    private int totalTrash = 10;  // Total trash count
    private int collectedTrash = 0;  // Count of collected trash
    private int score = 0;  // Tracks the total score

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"Time: {minutes:D2}:{seconds:D2}";
        }
        else
        {
            LoseGame();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
    }


    public void CollectTrash()
    {
        collectedTrash++;

        if (collectedTrash >= totalTrash)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        isGameOver = true;
        gameOverText.text = "YOU WIN!";
        SpawnWinObject(); 
        gameOverText.gameObject.SetActive(true);
        Invoke("RestartGame", 3f); // Restart after 3 seconds
    }
    void SpawnWinObject()
    {
        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.position = new Vector3(0, 2, 0); // Mid-air
        ball.transform.localScale = new Vector3(2, 2, 2); // Adjust size
    }

    void LoseGame()
    {
        isGameOver = true;
        gameOverText.text = "YOU LOSE!";
        SpawnLoseObject(); 
        gameOverText.gameObject.SetActive(true);
        Invoke("RestartGame", 3f);
    }
    void SpawnLoseObject()
    {
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        block.transform.position = new Vector3(0, 2, 0); // Mid-air
        block.transform.localScale = new Vector3(2, 2, 2); // Adjust size
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Restart the current scene
    }
}
