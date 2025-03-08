using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTrash : MonoBehaviour
{
    public int penaltyPoints = 10;  // Points deducted when collected

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            GameManager.Instance.RemovePoints(penaltyPoints);  // Deduct points
            GameManager.Instance.CollectTrash(false);  
            Destroy(gameObject);  // Destroy the bad trash object
        }
    }
}

