    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        

        [SerializeField] private TMP_Text timerText;  
        [SerializeField] private TMP_Text scoreText; 
        [SerializeField] private TMP_Text trashRemainingText;

        [Header("Game State Variables")]
        [SerializeField] private bool isGameOver = false;
        [SerializeField] private bool isFrozen = false;
        [SerializeField] private bool hasWon = false;
        [SerializeField] private bool isInWinDelay = false;



        [Header("Gameplay Progress & Score Tracking")]
        [SerializeField] private float timeRemaining;
        [SerializeField] private float timeSpent;
        [SerializeField] private int totalTrash = 10; // expected number of trash to be collected, the acutal "total nunmber" may be larger
        [SerializeField] private int collectedTrash = 0;
        [SerializeField] private int score = 0;
        [SerializeField] private string currentLevel; 

        [Header("UI Panels for Win/Lose")]
        [SerializeField] private GameObject winMessageUI;
        [SerializeField] private TMP_Text winMessageText;
        [SerializeField] private GameObject loseMessageUI; 
        [SerializeField] private GameObject helpUI;



        [Header("UI Panel for Next Level Instruction")]
        [SerializeField] private GameObject nextLevelInstructionUI;

        [Header("Door & Camera Logic")]
        [SerializeField] private Camera mainCamera;   
        [SerializeField] private Transform cameraBehindRobot;

        [Header("Win UI Elements")]
        [SerializeField] private GameObject restartButton;  

        
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
            currentLevel = scene.name;

            // Full reset of game state
            collectedTrash = 0;
            score = 0;
            timeSpent = 0f;
            hasWon = false;
            isGameOver = false;
            isFrozen = false;
            isInWinDelay = false;

            SetLevelTime(scene);  // Also resets timer
            UpdateUIElements();  // Re-link UI in the new scene
            // Show Help only on Level1
            if (scene.name == "Level1")
            {
                GameObject helpObj = GameObject.Find("Help");
                if (helpObj != null)
                {
                    helpUI = helpObj;
                    helpUI.SetActive(true);
                    StartCoroutine(HideHelpAfterDelay(7f));
                }
                // Freeze player and timer for 7 seconds at start of Level 1
                StartCoroutine(DelayStartForLevel1());
            }

            UpdateTrashRemainingText();
            UpdateScoreText();

            // Hide UI panels freshly on scene load
            if (winMessageUI) winMessageUI.SetActive(false);
            if (loseMessageUI) loseMessageUI.SetActive(false);
            if (nextLevelInstructionUI) nextLevelInstructionUI.SetActive(false);

            Time.timeScale = 1f; // Ensure game is running            

        }

        private System.Collections.IEnumerator DelayStartForLevel1()
        {
            isFrozen = true;

            RobotMovement player = FindFirstObjectByType<RobotMovement>();


            if (player != null)
                player.SetControlEnabled(false);

            yield return new WaitForSecondsRealtime(7f);

            isFrozen = false;

            if (player != null)
                player.SetControlEnabled(true);
        }


        

        public void FreezeGame()
        {
            isFrozen = true; 
            Time.timeScale = 0f;  // Pause game logic
        }

        public void UnfreezeGame()
        {
            isFrozen = false;
            Time.timeScale = 1f;  // Resume game logic
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
            if (!isGameOver && !isFrozen && !hasWon)
            {
                UpdateTimer();
            }
            // Only allow input when on Level3 and win message is active
            if (currentLevel == "Level3" && isGameOver && nextLevelInstructionUI != null && nextLevelInstructionUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    RestartGame();
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    GoToPreviousLevel();
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    SceneManager.LoadScene("Level1");
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OnExitButton();
                }
            }
        }

        void UpdateTimer()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeSpent += Time.deltaTime;
                
                // Only update UI if we have a valid reference
                if (timerText != null)
                {
                    timerText.text = $"Time: {timeRemaining:F2}";
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
            if (isGameOver || hasWon) return; // do nothing if game is over. 

            if (isGoodTrash)
            {
                collectedTrash++;
            }
            else
            {
                collectedTrash--;
                if (collectedTrash < 0) collectedTrash = 0;
            }
            UpdateTrashRemainingText();

            if (!isGameOver && collectedTrash >= totalTrash)
            {
                StartCoroutine(DelayedWinCoroutine());
            }
        }

        private System.Collections.IEnumerator DelayedWinCoroutine()
        {
            isInWinDelay = true;

            float delay = 5f;
            float startTime = Time.realtimeSinceStartup;

            while (Time.realtimeSinceStartup - startTime < delay)
            {
                yield return null;  // Wait frame by frame
            }

            if (!isGameOver && !hasWon)
            {
                WinGame();
            }

            isInWinDelay = false; 
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
            hasWon = true;

            string rank = EvaluatePerformance(); 

            if (winMessageText != null)
            {
                winMessageText.text = $"You Win! {rank}";
            }

            if (winMessageUI != null)
            {
                winMessageUI.SetActive(true);
            }

            if (currentLevel == "Level3" && restartButton != null)
            {
                restartButton.SetActive(true);
            }

            StartCoroutine(HideWinAfterDelay(3f));
        }


        private string EvaluatePerformance()
        {
            string rank;

            if (score >= 130 && timeSpent <= 30)
            {
                rank = "SSS Rank";
            }
            else if (score >= 100 && timeSpent <= 30)
            {
                rank = "SS Rank";
            }
            else if (timeSpent <= 30)
            {
                rank = "S Rank";
            }
            else if (timeSpent  <= 40)
            {
                rank = "A Rank";
            }
            else if (timeSpent >= 50)
            {
                rank = "B Rank";
            }
            else if (timeSpent <= 60)
            {
                rank = "C Rank";
            }
            else if (score >= 100) {
                rank = "D+ Rank";
            }
            else
            {
                rank = "D Rank";
            }

            return rank;
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
            if (isInWinDelay) return;  // Prevent losing during win delay

            isGameOver = true;
            if (loseMessageUI != null) loseMessageUI.SetActive(true);
            StartCoroutine(HideLoseAfterDelay(10f));
            FreezeGame();
        }


        void RestartGame()
        {
            if (loseMessageUI != null) loseMessageUI.SetActive(false);
            UnfreezeGame();

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
            // Re-find all UI elements in the new scene
            GameObject timeTextObj = GameObject.Find("Time Text");
            if (timeTextObj != null)
            {
                timerText = timeTextObj.GetComponent<TMP_Text>();
            }

            GameObject scoreTextObj = GameObject.Find("ST");
            if (scoreTextObj != null)
            {
                scoreText = scoreTextObj.GetComponent<TMP_Text>();
            }

            GameObject trashTextObj = GameObject.Find("Trash Until Win");
            if (trashTextObj != null)
            {
                trashRemainingText = trashTextObj.GetComponent<TMP_Text>();
            }

            GameObject winUIObj = GameObject.Find("WinMessageUI");
            if (winUIObj != null)
            {
                winMessageUI = winUIObj;
                winMessageUI.SetActive(false);

                // Try to find TMP_Text component on it or inside it
                winMessageText = winUIObj.GetComponent<TMP_Text>();
                if (winMessageText == null)
                {
                    winMessageText = winUIObj.GetComponentInChildren<TMP_Text>();
                }
            }

            GameObject loseUIObj = GameObject.Find("LoseMessageUI");
            if (loseUIObj != null)
            {
                loseMessageUI = loseUIObj;
                loseMessageUI.SetActive(false); // Hide again
            }

            GameObject nextLevelUIObj = GameObject.Find("NextLevelInstructionUI");
            if (nextLevelUIObj != null)
            {
                nextLevelInstructionUI = nextLevelUIObj;
                nextLevelInstructionUI.SetActive(false); // Hide again
            }


            if (mainCamera == null)
            {
                Camera cam = Camera.main;
                if (cam != null)
                {
                    mainCamera = cam;
                }
            }

            GameObject cameraTargetObj = GameObject.Find("CameraTarget");

            if (cameraTargetObj != null)
            {
                cameraBehindRobot = cameraTargetObj.transform;
            }

            UpdateScoreText();

        }

        private void UpdateTrashRemainingText()
        {
            if (trashRemainingText != null)
            {
                int remaining = totalTrash - collectedTrash;
                trashRemainingText.text = $"Trash Until Win: {remaining}";
            }
        }



        // Button callback: player clicked restart
        public void OnRestartButton()
        {
            RestartGame();
        }

        // Button call back: player clicked previous level
        public void GoToPreviousLevel()
        {
            if (currentLevel == "Level2")
            {
                SceneManager.LoadScene("Level1");
            }
            else if (currentLevel == "Level3")
            {
                SceneManager.LoadScene("Level2");
            }
            else
            {
                Debug.Log("Already at the first level. Cannot go back further.");
            }
        }




        // Button callback: player clicked exit
        public void OnExitButton()
        {   
            // hide the UI
            if (winMessageUI != null) winMessageUI.SetActive(false);
            if (loseMessageUI != null) loseMessageUI.SetActive(false);
            if (nextLevelInstructionUI != null) nextLevelInstructionUI.SetActive(false);

            // Reset UI text
            if (timerText != null) timerText.text = "Time: 00:00";
            if (scoreText != null) scoreText.text = "Score: 0";

            // Optional: Reset game state
            isGameOver = false;
            hasWon = false;
            score = 0;
            collectedTrash = 0;

            Application.Quit();
        }

        // Door stuff
        public void OnPlayerInteractsDoor()
        {
            StartCoroutine(AdvanceToNextLevelRoutine());
        }

        private System.Collections.IEnumerator AdvanceToNextLevelRoutine()
        {
            if (nextLevelInstructionUI != null) nextLevelInstructionUI.SetActive(false);

            if (mainCamera != null && cameraBehindRobot != null)
            {
                mainCamera.transform.position = cameraBehindRobot.position;
                mainCamera.transform.rotation = cameraBehindRobot.rotation;
            }

            yield return new WaitForSeconds(1f);

            LoadNextLevel();
        }    


        // Hide win UI and show instruction
        private System.Collections.IEnumerator HideWinAfterDelay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (winMessageUI != null) winMessageUI.SetActive(false);
            if (nextLevelInstructionUI != null) nextLevelInstructionUI.SetActive(true);
            UnfreezeGame();
        }

        // Hide lose UI after time if player didn't restart
        private System.Collections.IEnumerator HideLoseAfterDelay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (loseMessageUI != null) loseMessageUI.SetActive(false);
            UnfreezeGame(); 
        }

        private System.Collections.IEnumerator HideHelpAfterDelay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (helpUI != null)
            {
                helpUI.SetActive(false);
            }
        }

    }