// GameManager.cs Tianyou Liu, Nian Gao, Alina Pan
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To restart the game
using TMPro; 


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float timeRemaining; // time remaining variable
    private bool isGameOver = false;
    public TMP_Text timerText;  // Assign in Inspector (Timer UI)

    [SerializeField] private int totalTrash = 10;  // Total trash count
    [SerializeField] private int collectedTrash = 0;  // Count of collected trash
    [SerializeField] private int score = 0;  // Tracks the total score

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetLevelTime(scene); // Ensure the correct time is set when switching scenes
        isGameOver = false; // Reset game to not be over when switching scenes
    }

    private void SetLevelTime(Scene scene) 
    {
        string sceneName = scene.name;

        if (sceneName == "Level1") // Level 1
        {
            timeRemaining = 180f; 
        }
        else if (sceneName == "Level2") // Level 2
        {
            timeRemaining = 120f;
        }
        else
        {
            timeRemaining = 180f; // Default value for other scenes for now
        }
        collectedTrash = 0;  // Reset collected trash everytime a new scene is loaded
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
        Invoke("LoadNextLevel", 3f); // go to next level 
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("Level2"); // Load Level 2 after win screen
    }
    // in the future this function might be edited to enable loading further scenes. 


    void LoseGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("LoseMessageScene");
        Invoke("RestartGame", 3f);
    }


    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Restart the current scene
    }

    public float GetTimeRemaining() {
        return timeRemaining;
    }
}
