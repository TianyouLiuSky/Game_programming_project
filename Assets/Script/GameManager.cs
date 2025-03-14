using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float timeRemaining;
    private bool isGameOver = false;
    private TMP_Text timerText;  // Will be found at runtime

    [SerializeField] private int totalTrash = 10;
    [SerializeField] private int collectedTrash = 0;
    [SerializeField] private int score = 0;

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
        SetLevelTime(scene);
        isGameOver = false;
        
        // Find and assign the timer text in the new scene
        GameObject timeTextObj = GameObject.Find("Time Text");
        if (timeTextObj != null)
        {
            timerText = timeTextObj.GetComponent<TMP_Text>();
        }
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
        else
        {
            timeRemaining = 180f; // Default value for other scenes
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
    }

    void WinGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("WinMessageScene");
        Invoke("LoadNextLevel", 3f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    void LoseGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("LoseMessageScene");
        Invoke("RestartGame", 3f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}