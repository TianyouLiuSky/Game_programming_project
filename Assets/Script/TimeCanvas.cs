using UnityEngine;
using TMPro;

public class TimeCanvas : MonoBehaviour
{
    [SerializeField] private GameObject timeText;  // Keep as GameObject reference
    private TMP_Text textComponent;  // Separate variable for the TMP component

    void Start()
    {
        // Check if timeText is assigned
        if (timeText == null)
        {
            Debug.LogError("Time Text GameObject is not assigned!");
            enabled = false;
            return;
        }

        // Get the TextMeshPro component
        textComponent = timeText.GetComponent<TMP_Text>();
        
        // Check if we found the TextMeshPro component
        if (textComponent == null)
        {
            Debug.LogError("No TextMeshPro component found on Time Text GameObject!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && textComponent != null)
        {
            textComponent.text = "Remaining time: " + GameManager.Instance.GetTimeRemaining();
        }
    }
}