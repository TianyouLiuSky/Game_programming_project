using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BadTrash : MonoBehaviour
{
    [Header("Trash score")]
    [SerializeField] private int penaltyPoints = 10;  // Points deducted when collected
    [SerializeField] private AudioClip collectSound; // Sound effect

    [Header("Visual Feedback")]
    [SerializeField] private GameObject floatingTextPrefab; // Prefab for the floating text


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
            // Show floating text
            if (floatingTextPrefab != null)
            {
                Vector3 textPosition = transform.position + new Vector3(0, 0, 0);
                GameObject textObject = Instantiate(floatingTextPrefab, textPosition, Quaternion.identity);
                TextMeshPro textMesh = textObject.GetComponent<TextMeshPro>();
                if (textMesh != null)
                {
                    textMesh.text = "-" + penaltyPoints.ToString();
                    textObject.AddComponent<FloatingTextHelper>();
                }
            }
            Destroy(gameObject);  // Destroy the bad trash object
        }
    }
}

