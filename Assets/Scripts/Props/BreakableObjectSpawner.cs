using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectSpawner : MonoBehaviour
{
    public GameObject boxesPrefab;
    public GameObject barrelsPrefab;
    public List<Transform> spawnPoints;
    private List<Transform> usedSpawnPoints = new List<Transform>();

    private int currentTotalBreakables = 0;
    public int maxTotalBreakables = 8;

    private void OnEnable()
    {
        BreakableParent.breakableBroke += MinusBreakable;
    }

    private void OnDisable()
    {
        BreakableParent.breakableBroke -= MinusBreakable;
    }

    private void Start()
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

    public void SpawnBreakables()
    {
        // If the current round allows chest spawning, and the chest limit is not exceeded
        if (currentTotalBreakables < maxTotalBreakables)
        {
            foreach (Transform spawnedBreakable in spawnPoints)
            {
                // Check if the spawn point is already used for a chest or if the max total chests are reached
                if (usedSpawnPoints.Contains(spawnedBreakable) || currentTotalBreakables >= maxTotalBreakables)
                    continue;

                // Randomly choose between boxesPrefab and barrelsPrefab
                GameObject prefabToSpawn = Random.Range(0, 2) == 0 ? boxesPrefab : barrelsPrefab;

                // Spawn the chosen prefab at the current spawn point
                GameObject newBreakable = Instantiate(prefabToSpawn, spawnedBreakable.position, spawnedBreakable.rotation);

                // Set the spawned prefab as a child of the current spawn point
                newBreakable.transform.parent = spawnedBreakable;
                usedSpawnPoints.Add(spawnedBreakable);
                currentTotalBreakables++;
            }
        }
    }

    public void MinusBreakable()
    {
        currentTotalBreakables--;
    }
}
