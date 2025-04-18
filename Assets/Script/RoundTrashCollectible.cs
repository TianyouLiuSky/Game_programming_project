using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTrashCollectible : MonoBehaviour
{
    [Header("Trash scores")]
    [SerializeField] private int points = 10;  // Points per collectible
    [SerializeField] private AudioClip collectSound; // Sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the robot has the "Player" tag
        {
            GameManager.Instance.AddPoints(points); 
            GameManager.Instance.CollectTrash(true);  // Add points
            if (collectSound != null)
            {
                // Play the sound effect
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            Destroy(gameObject);  // Destroy the collectible
        }
    }
}
