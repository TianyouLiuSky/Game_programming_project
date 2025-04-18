using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTrash : MonoBehaviour
{
    [Header("Trash score")]
    [SerializeField] private int penaltyPoints = 10;  // Points deducted when collected
    [SerializeField] private AudioClip collectSound; // Sound effect


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            GameManager.Instance.RemovePoints(penaltyPoints);  // Deduct points
            GameManager.Instance.CollectTrash(false);  
            if (collectSound != null)
            {
                // Play the sound effect
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            Destroy(gameObject);  // Destroy the bad trash object
        }
    }
}

