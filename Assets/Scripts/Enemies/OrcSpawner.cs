using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcSpawner : MonoBehaviour
{
    public GameObject orcPrefab;
    public Transform spawnPointsContainer;
    public float spawnDuration = 15.0f;
    public Transform parentTransform;
    public bool startToSpawnOrcs = false;

    private List<Transform> spawnPoints = new List<Transform>();
    private float timeBetweenSpawns;
    private float spawnTimer = 0.0f;

    private void Start()
    {
        // Populate the spawnPoints list with child transforms from the spawnPointsContainer
        foreach (Transform spawnPoint in spawnPointsContainer)
        {
            spawnPoints.Add(spawnPoint);
        }

        // Calculate the time interval between orc spawns
        timeBetweenSpawns = spawnDuration / RoundManager.Instance.orcsToSpawnInCurrentRound;
    }

    private void Update()
    {
        if (!startToSpawnOrcs) { return; }

        // Only start spawning orcs if not all orcs are spawned yet
        if (RoundManager.Instance.orcsSpawnedInCurrentRound < RoundManager.Instance.orcsToSpawnInCurrentRound)
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
        if (RoundManager.Instance.orcsSpawnedInCurrentRound < RoundManager.Instance.orcsToSpawnInCurrentRound)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instantiate the orc prefab
            GameObject newOrc = Instantiate(orcPrefab, spawnPoint.position, spawnPoint.rotation);

            // Set the parent transform to the assigned parent
            newOrc.transform.SetParent(parentTransform);

            RoundManager.Instance.OrcSpawned(); // Inform the RoundManager that an orc is spawned
        }
    }
}
