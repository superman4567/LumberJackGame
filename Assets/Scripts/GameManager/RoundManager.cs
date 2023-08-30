using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // Singleton instance
    public static RoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public int currentRound = 0;
    public int orcsToSpawnInCurrentRound;
    public int orcsSpawnedInCurrentRound = 0;
    public int orcsKilledInCurrentRound = 0;
    public float orcSpawnIncreasePercentage;

    private void Start()
    {
        Statemachine();
        StartCoroutine(StartNewRoundCoroutine());
    }

    private void Update()
    {
        Debug.Log("Current round =" + currentRound);
        if (orcsSpawnedInCurrentRound == orcsToSpawnInCurrentRound && orcsKilledInCurrentRound == orcsToSpawnInCurrentRound)
        {
            StartCoroutine(StartNewRoundCoroutine());
        }
    }

    private IEnumerator StartNewRoundCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed

        orcsToSpawnInCurrentRound = Mathf.CeilToInt(orcsToSpawnInCurrentRound * (1 + orcSpawnIncreasePercentage));
        Debug.Log(orcsToSpawnInCurrentRound);

        orcsSpawnedInCurrentRound = 0;
        orcsKilledInCurrentRound = 0;

        currentRound++;
    }

    private void Statemachine()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                orcSpawnIncreasePercentage = 1.5f;
                break;

            case 1:
                orcSpawnIncreasePercentage = 3f;
                break;

            case 2:
                orcSpawnIncreasePercentage = 7.5f;
                break;

            default:
                break;
        }
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
