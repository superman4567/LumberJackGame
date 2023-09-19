using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool limitReached = false;
    private int spawnedorc = 0;

    private void Start()
    {
        // Populate the spawnPoints list with child transforms from the spawnPointsContainer
        foreach (Transform spawnPoint in spawnPointsContainer)
        {
            spawnPoints.Add(spawnPoint);
        }

        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            // Calculate the time interval between orc spawns
            timeBetweenSpawns = spawnDuration / RoundManager.Instance.OrcsSpawnedThisRound();
        }
            
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 3 && startToSpawnOrcs && !limitReached)
        {
            SpawnOrcOnce(1);
        }    

        if (SceneManager.GetActiveScene().buildIndex == 3) { return; }

        if (!startToSpawnOrcs) { return; }

        // Only start spawning orcs if not all orcs are spawned yet
        if (RoundManager.Instance.orcsSpawnedInCurrentRound < RoundManager.Instance.OrcsSpawnedThisRound())
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
        if (SceneManager.GetActiveScene().buildIndex == 3) { return; }

            if (RoundManager.Instance.orcsSpawnedInCurrentRound < RoundManager.Instance.OrcsSpawnedThisRound())
            {
                int randomIndex = Random.Range(0, spawnPoints.Count);
                Transform spawnPoint = spawnPoints[randomIndex];

                // Instantiate the orc prefab
                GameObject newOrc = Instantiate(orcPrefab, spawnPoint.position, spawnPoint.rotation);   

                // Set the parent transform to the assigned parent
                newOrc.transform.SetParent(parentTransform);

                // Generate a random scale factor within the range [0.75, 1.25]
                float randomScale = Random.Range(0.85f, 1.15f);

                // Apply the random scale uniformly to all axes
                Vector3 newScale = new Vector3(randomScale, randomScale, randomScale);
                newOrc.transform.localScale = newScale;

                RoundManager.Instance.OrcSpawned(); // Inform the RoundManager that an orc is spawned
            }
    }

    private void SpawnOrcOnce(int amount)
    {
        if (spawnedorc >= 2) { return; }

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instantiate the orc prefab
            GameObject newOrc = Instantiate(orcPrefab, spawnPoint.position, spawnPoint.rotation);

            // Set the parent transform to the assigned parent
            newOrc.transform.SetParent(parentTransform);

            // Generate a random scale factor within the range [0.75, 1.25]
            float randomScale = Random.Range(0.85f, 1.15f);

            // Apply the random scale uniformly to all axes
            Vector3 newScale = new Vector3(randomScale, randomScale, randomScale);
            newOrc.transform.localScale = newScale;

            spawnedorc++;
        }
    }
}
