using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }
    public int currentRound = 0;
    public int orcsToSpawnInCurrentRound;
    public int roundToCompleteLevel;
    public int orcsSpawnedInCurrentRound = 0;
    public int orcsKilledInCurrentRound = 0;
    public float orcSpawnIncreasePercentage;
    private bool isStartingNewRound = false; 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Statemachine();
        currentRound++;
    }

    private void Update()
    {
        if (!isStartingNewRound && orcsKilledInCurrentRound == orcsToSpawnInCurrentRound)
        {
            IsDifficultyComplete();
            StartCoroutine(StartNewRoundCoroutine());
        }
    }

    private IEnumerator StartNewRoundCoroutine()
    {
        isStartingNewRound = true; // Set the flag to prevent multiple calls

        yield return new WaitForSeconds(1.0f);

        orcsToSpawnInCurrentRound = Mathf.CeilToInt(orcsToSpawnInCurrentRound * orcSpawnIncreasePercentage);
        orcsSpawnedInCurrentRound = 0;
        orcsKilledInCurrentRound = 0;

        currentRound++;

        isStartingNewRound = false; // Reset the flag after the coroutine finishes
    }

    private void Statemachine()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                orcSpawnIncreasePercentage = 1.5f;
                orcsToSpawnInCurrentRound = 10;
                roundToCompleteLevel = 20;
                break;

            case 1:
                orcSpawnIncreasePercentage = 3f;
                orcsToSpawnInCurrentRound = 25;
                roundToCompleteLevel = 50;
                break;

            case 2:
                orcSpawnIncreasePercentage = 7.5f;
                orcsToSpawnInCurrentRound = 40;
                roundToCompleteLevel = 100;
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

    private void IsDifficultyComplete()
    {
        if (currentRound == roundToCompleteLevel)
        {
            Time.timeScale = 0;
            //show UI
        }
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
