using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashPrefab;  // Assign your Trash prefab in Inspector
    public int numberOfTrash = 10;  // Number of trash objects to spawn
    public Vector3 spawnArea = new Vector3(10, 1, 10); // Define spawn area (X, Y, Z)

    void Start()
    {
        SpawnTrash();
    }

    void SpawnTrash()
    {
        for (int i = 0; i < numberOfTrash; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),  // X within range
                spawnArea.y,  // Y height (adjust as needed)
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)   // Z within range
            );

            Instantiate(trashPrefab, randomPosition, Quaternion.identity);
        }
    }
}
