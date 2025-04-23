using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private bool playerInTrigger = false;

    [SerializeField] private bool hasPressedO = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (!hasPressedO && playerInTrigger && Input.GetKeyDown(KeyCode.O))
        {
            hasPressedO = true;
            GameManager.Instance.OnPlayerInteractsDoor();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.OnRestartButton();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.GoToPreviousLevel();
        }        
    }
}

