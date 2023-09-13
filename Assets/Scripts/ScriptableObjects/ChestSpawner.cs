using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;
    public List<Transform> spawnPoints;
    private List<Transform> usedSpawnPoints = new List<Transform>();

    private int currentTotalChests = 0;
    public int maxTotalChests = 3;

    private void OnEnable()
    {
        Chest.destroyChestAction += DestroyChest;
    }

    private void OnDisable()
    {
        Chest.destroyChestAction -= DestroyChest;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SpawnChest();
        }
        else
        {
            // Shuffle the spawnPoints list using Fisher-Yates shuffle algorithm
            for (int i = spawnPoints.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                Transform temp = spawnPoints[i];
                spawnPoints[i] = spawnPoints[randomIndex];
                spawnPoints[randomIndex] = temp;
            }
        }
    }

    public void SpawnChest()
    {
        // If the current round allows chest spawning, and the chest limit is not exceeded
        if (currentTotalChests < maxTotalChests)
        {
            foreach (Transform spawnedChest in spawnPoints)
            {
                // Check if the spawn point is already used for a chest or if the max total chests are reached
                if (usedSpawnPoints.Contains(spawnedChest) || currentTotalChests >= maxTotalChests)
                    continue;

                // Spawn a chest at the current spawn point
                GameObject newChest = Instantiate(chestPrefab, spawnedChest.position, spawnedChest.rotation);

                // Set the spawned chest as a child of the current spawn point
                newChest.transform.parent = spawnedChest;
                usedSpawnPoints.Add(spawnedChest);
                currentTotalChests++;
            }
        }
    }

    public void DestroyChest()
    {
        currentTotalChests--;
    }
}
