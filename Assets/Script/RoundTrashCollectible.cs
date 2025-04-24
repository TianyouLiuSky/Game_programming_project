using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundTrashCollectible : MonoBehaviour
{
    [Header("Trash scores")]
    [SerializeField] private int points = 10;  // Points per collectible
    [SerializeField] private AudioClip collectSound; // Sound effect
    
    [Header("Visual Feedback")]
    [SerializeField] private GameObject floatingTextPrefab; // Prefab for the floating text

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the robot has the "Player" tag
        {
            GameManager.Instance.AddPoints(points); 
            GameManager.Instance.CollectTrash(true);  // Add points
            
            // Play sound effect
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            // Show floating text
            if (floatingTextPrefab != null)
            {
                Vector3 textPosition = transform.position + new Vector3(0, 0, 0);
                GameObject textObject = Instantiate(floatingTextPrefab, textPosition, Quaternion.identity);
                TextMeshPro textMesh = textObject.GetComponent<TextMeshPro>();
                if (textMesh != null)
                {
                    textMesh.text = "+" + points.ToString();
                    FloatingTextHelper helper = textObject.AddComponent<FloatingTextHelper>();
                }
            }
            
            Destroy(gameObject);  // Destroy the collectible
        }
    }
}