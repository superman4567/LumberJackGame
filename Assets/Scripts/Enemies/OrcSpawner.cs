using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcSpawner : MonoBehaviour
{
    public GameObject orcPrefab;
    public Transform spawnPointsContainer;
    public float spawnDuration = 15.0f; // Total duration for spawning orcs
    public Transform parentTransform; // Assign the parent transform in the Inspector

    private List<Transform> spawnPoints = new List<Transform>();
    private RoundManager roundManager;
    private int currentRound = 1;
    private int orcsToSpawnInCurrentRound = 10;
    private int orcsSpawnedInCurrentRound = 0;
    private int orcsKilledInCurrentRound = 0;
    private float orcSpawnIncreasePercentage = 0.25f;
    private float timeBetweenSpawns; // Time interval between orc spawns
    private float spawnTimer = 0.0f; // Timer for tracking spawning progress

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
    }

    private void Start()
    {
        Debug.Log(orcsToSpawnInCurrentRound);
        // Populate the spawnPoints list with child transforms from the spawnPointsContainer
        foreach (Transform spawnPoint in spawnPointsContainer)
        {
            spawnPoints.Add(spawnPoint);
        }

        // Calculate the time interval between orc spawns
        timeBetweenSpawns = spawnDuration / orcsToSpawnInCurrentRound;
    }

    private void Update()
    {
        // Only start spawning orcs if not all orcs are spawned yet
        if (orcsSpawnedInCurrentRound < orcsToSpawnInCurrentRound)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= timeBetweenSpawns)
            {
                SpawnOrc();
                spawnTimer = 0.0f;
            }
        }
    }

    private void SpawnOrc()
    {
        if (orcsSpawnedInCurrentRound < orcsToSpawnInCurrentRound)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instantiate the orc prefab
            GameObject newOrc = Instantiate(orcPrefab, spawnPoint.position, spawnPoint.rotation);

            // Set the parent transform to the assigned parent
            newOrc.transform.SetParent(parentTransform);

            roundManager.OrcSpawned(); // Inform the RoundManager that an orc is spawned

            // Increment the counter AFTER spawning an orc
            orcsSpawnedInCurrentRound++;
        }
    }
}
