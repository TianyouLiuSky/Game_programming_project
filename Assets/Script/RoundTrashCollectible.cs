using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTrashCollectible : MonoBehaviour
{
    public int points = 10;  // Points per collectible
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the robot has the "Player" tag
        {
            GameManager.Instance.AddPoints(points);  // Add points
            Destroy(gameObject);  // Destroy the collectible
        }
    }
}
