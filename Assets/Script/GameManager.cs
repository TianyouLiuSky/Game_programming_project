// GameManager.cs Tianyou Liu, Nian Gao, Alina Pan
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To restart the game

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float timeRemaining = 180f; // 3 minutes (180 seconds)
    private bool isGameOver = false;
    public Text timerText;  // Assign in Inspector (Timer UI)

    [SerializeField] private int totalTrash = 10;  // Total trash count
    [SerializeField] private int collectedTrash = 0;  // Count of collected trash
    [SerializeField] private int score = 0;  // Tracks the total score

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


    public void CollectTrash(bool isGoodTrash)
    {
        if (isGoodTrash)
        {
            collectedTrash++;  // Increase count for good trash
        }
        else
        {
            collectedTrash--;  // Decrease count for bad trash (prevent going negative)
            if (collectedTrash < 0) collectedTrash = 0;
        }

        if (collectedTrash >= totalTrash)  // Player wins if they collect enough good trash
        {
            WinGame();
        }
    }

    public void RemovePoints(int points)
    {
        score -= points;
        if (score < 0) score = 0;  // Prevent negative scores
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

    public float GetTimeRemaining() {
        return timeRemaining;
    }
}
