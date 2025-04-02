using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float timeRemaining;
    private bool isGameOver = false;
    private TMP_Text timerText;  
    private TMP_Text scoreText; 


    [SerializeField] private int totalTrash = 10;
    [SerializeField] private int collectedTrash = 0;
    [SerializeField] private int score = 0;

    private string currentLevel; // add a new variable to track the current level

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
        // Save non-win/lose scene name and reset level data
        if (scene.name != "WinMessageScene" && scene.name != "LoseMessageScene")
        {
            currentLevel = scene.name;
            // Reset level data
            collectedTrash = 0;
            score = 0;  // Reset score at the start of each level
        }
        
        SetLevelTime(scene);
        isGameOver = false;
        
        // Update UI elements
        UpdateUIElements();
    }

    private void SetLevelTime(Scene scene)
    {
        string sceneName = scene.name;

        if (sceneName == "Level1")
        {
            timeRemaining = 180f;
        }
        else if (sceneName == "Level2")
        {
            timeRemaining = 120f;
        }
        else if (sceneName == "Level3")
        {
            timeRemaining = 90f;  // less time for level 3
        }
        else
        {
            timeRemaining = 1800f; // Default value for other scenes, very long time.
        }
        collectedTrash = 0;
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
            
            // Only update UI if we have a valid reference
            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerText.text = $"Time: {minutes:D2}:{seconds:D2}";
            }
        }
        else
        {
            LoseGame();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void CollectTrash(bool isGoodTrash)
    {
        if (isGoodTrash)
        {
            collectedTrash++;
        }
        else
        {
            collectedTrash--;
            if (collectedTrash < 0) collectedTrash = 0;
        }

        if (collectedTrash >= totalTrash)
        {
            WinGame();
        }
    }

    public void RemovePoints(int points)
    {
        score -= points;
        if (score < 0) score = 0;
        UpdateScoreText();
    }

    void WinGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("WinMessageScene");
        Invoke("LoadNextLevel", 3f);
    }

    void LoadNextLevel()
    {
        if (currentLevel == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (currentLevel == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
    }

    void LoseGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("LoseMessageScene");
        Invoke("RestartGame", 3f);
    }

    void RestartGame()
    {
        // store the current scene information
        if (!string.IsNullOrEmpty(currentLevel))
        {
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            SceneManager.LoadScene("Level1"); // default to scene 1
        }
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    // Clean up when the object is destroyed
    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    // UI updating
    private void UpdateUIElements()
    {
        // Find and setup timer text
        GameObject timeTextObj = GameObject.Find("Time Text");
        if (timeTextObj != null)
        {
            timerText = timeTextObj.GetComponent<TMP_Text>();
        }

        // Find and setup score text
        GameObject scoreTextObj = GameObject.Find("ST");
        if (scoreTextObj != null)
        {
            scoreText = scoreTextObj.GetComponent<TMP_Text>();
        }
        
        UpdateScoreText();
    }

}