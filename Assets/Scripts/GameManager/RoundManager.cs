using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [Header("Round Switch panel")]
    [SerializeField] private TextMeshProUGUI currentRoundNumber;
    [SerializeField] private TextMeshProUGUI nextRoundNumber;
    [SerializeField] private GameObject roundSwitchPanel;
    [SerializeField] private Animator nextRoundAnimator;

    [Header("Difficulty Complete panel")]
    [SerializeField] private GameObject difficultyCompletePanel;
    [SerializeField] private Animator difficultyCompleteAnimator;

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
            StartCoroutine(StartNewRoundCoroutine());
            IsDifficultyComplete();
        }
    }

    private IEnumerator StartNewRoundCoroutine()
    {
        isStartingNewRound = true; // Set the flag to prevent multiple calls
        RoundCompplete(currentRound);

        yield return new WaitForSeconds(5.0f);

        orcsToSpawnInCurrentRound = Mathf.CeilToInt(orcsToSpawnInCurrentRound * orcSpawnIncreasePercentage);
        orcsSpawnedInCurrentRound = 0;
        orcsKilledInCurrentRound = 0;

        currentRound++;

        isStartingNewRound = false; 
    }

    private void Statemachine()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                orcSpawnIncreasePercentage = 2f; //1.5
                orcsToSpawnInCurrentRound = 1; // 10
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

    private void RoundCompplete(int currentRound)
    {
        currentRoundNumber.text = currentRound.ToString();
        nextRoundNumber.text = (currentRound + 1).ToString();
        nextRoundAnimator.SetBool("NextRound", true);
        Invoke("ResetRoundComplete", 5f);
    }

    private void ResetRoundComplete()
    {
        nextRoundAnimator.SetBool("NextRound", false);
    }

    private void IsDifficultyComplete()
    {
        if (currentRound == roundToCompleteLevel)
        {
            if (GameManager.Instance.GetDifficulty() == 0)
            {
                GameManager.Instance.unlockedDifficultyList[1] = true;
            }
            else if (GameManager.Instance.GetDifficulty() == 1)
            {
                GameManager.Instance.unlockedDifficultyList[2] = true;
            }
            else if (GameManager.Instance.GetDifficulty() == 2)
            {
                //thank you for playing
            }
            
            Time.timeScale = 0;
            //show UI that the level with current difficulty is complete
            difficultyCompletePanel.SetActive(true);
        }
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
