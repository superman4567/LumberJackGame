using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;
    public List<Transform> spawnPoints; // List of spawn points

    public int maxTotalChests = 3; // Maximum total chests allowed in the game
    private int currentTotalChests = 0; // Current total chests in the game
    private int maxChestsPerRound = 3; // Maximum chests to spawn per round
    private int chestsSpawnedInCurrentRound = 0; // Counter for chests spawned in the current round
    private int previousRound = -1; // Variable to store the previous round

    private RoundManager roundManager; // Reference to the RoundManager script
    private List<Transform> usedSpawnPoints = new List<Transform>(); // List of used spawn points

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>(); // Find the RoundManager script
    }

    void Start()
    {
        // If the current round allows chest spawning, and the chest limit is not exceeded
        if (roundManager.currentRound > 0 && chestsSpawnedInCurrentRound < maxChestsPerRound && currentTotalChests < maxTotalChests)
        {
            // Shuffle the spawnPoints list using Fisher-Yates shuffle algorithm
            for (int i = spawnPoints.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                Transform temp = spawnPoints[i];
                spawnPoints[i] = spawnPoints[randomIndex];
                spawnPoints[randomIndex] = temp;
            }

            // Loop through shuffled spawn points
            foreach (Transform spawnPoint in spawnPoints)
            {
                // Check if the spawn point is already used for a chest or if the max total chests are reached
                if (usedSpawnPoints.Contains(spawnPoint) || currentTotalChests >= maxTotalChests)
                    continue;

                // Spawn a chest at the current spawn point
                GameObject newChest = Instantiate(chestPrefab, spawnPoint.position, spawnPoint.rotation);

                // Set the spawned chest as a child of the current spawn point
                newChest.transform.parent = spawnPoint;

                // Mark the spawn point as used
                usedSpawnPoints.Add(spawnPoint);

                // Increment counters
                currentTotalChests++;
                chestsSpawnedInCurrentRound++;

                // Break the loop if max chests for the round have been spawned
                if (chestsSpawnedInCurrentRound >= maxChestsPerRound)
                    break;
            }
        }

        // Update the previous round variable
        previousRound = roundManager.currentRound;
    }
}
