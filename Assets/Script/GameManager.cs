// GameManager.cs Tianyou Liu, Nian Gao, Alina Pan
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To restart the game

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float timeRemaining = 180f; // 3 minutes (180 seconds)
    private bool isGameOver = false;
    public Text timerText;  // Assign in Inspector (Timer UI)

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

    void UpdateTimer() // update the timer, but not properly displayed because timer UI not working
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

        if (collectedTrash >= totalTrash) // if the player has collected all the trash, the player wins
        {
            WinGame();
        }
    }

    void WinGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("WinMessageScene");
        Invoke("RestartGame", 30f); // Restart after 30 seconds
    }


    void LoseGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("LoseMessageScene");
        Invoke("RestartGame", 30f);
    }


    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Restart the current scene
    }
}
