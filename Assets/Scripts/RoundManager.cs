using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public OrcSpawner orcSpawner; // Reference to the OrcSpawner script

    [SerializeField] private float speedIncreasePerRound = 0.5f;
    private int currentRound = 1;
    private int orcsToSpawnInCurrentRound = 10;
    private int orcsSpawnedInCurrentRound = 0;
    private int orcsKilledInCurrentRound = 0;
    private float orcSpawnIncreasePercentage = 0.25f;

    private void Start()
    {
        orcSpawner = GetComponent<OrcSpawner>(); // Get the reference to the OrcSpawner script
        StartNewRound();
    }

    private void Update()
    {
        if (orcsKilledInCurrentRound == orcsToSpawnInCurrentRound)
        {
            StartNewRound();
        }
    }

    private void StartNewRound()
    {
        Debug.Log("Current round =" + currentRound);
        currentRound++;
        orcsToSpawnInCurrentRound = Mathf.CeilToInt(orcsToSpawnInCurrentRound * (1 + orcSpawnIncreasePercentage));
        orcsSpawnedInCurrentRound = 0;
        orcsKilledInCurrentRound = 0;
    }

    public float GetCurrentRoundSpeedMultiplier()
    {
        // Calculate the speed multiplier based on the current round
        // You can adjust the formula as needed
        float speedMultiplier = 1 + (currentRound - 1) * speedIncreasePerRound;
        return speedMultiplier;
    }

    public void OrcSpawned()
    {
        orcsSpawnedInCurrentRound++;
    }

    public void OrcKilled()
    {
        orcsKilledInCurrentRound++;
    }
}
