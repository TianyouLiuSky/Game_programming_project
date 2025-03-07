using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCanvas : MonoBehaviour
{
    public GameObject timeText;
    private TMP_Text textComponent;

    void Start()
    {
        textComponent = timeText.GetComponent<TMP_Text>();   
    }
    void Update()
    {
        textComponent.text = "Remaining time: " + GameManager.Instance.GetTimeRemaining();
    }
}
