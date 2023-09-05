using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    [Header("Round Switch panel")]
    [SerializeField] private TextMeshProUGUI currentRoundNumber;
    [SerializeField] private TextMeshProUGUI nextRoundNumber;
    [SerializeField] private GameObject roundSwitchPanel;
    [SerializeField] private Animator nextRoundAnimator;

    [Header("Difficulty Complete panel")]
    [SerializeField] private GameObject difficultyCompletePanel;

    [Header("Difficulty anim panels")]
    [SerializeField] private GameObject diffAnimPanel0;
    [SerializeField] private GameObject diffAnimPanel1;
    [SerializeField] private GameObject diffAnimPanel2;
    
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

        difficultyCompletePanel.SetActive(false);
        diffAnimPanel0.SetActive(false); 
        diffAnimPanel1.SetActive(false);
        diffAnimPanel2.SetActive(false);
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
                orcSpawnIncreasePercentage = 1.5f; //1.5
                orcsToSpawnInCurrentRound = 10; // 10
                roundToCompleteLevel = 25;
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
                difficultyCompletePanel.SetActive(true);
                diffAnimPanel0.SetActive(true);
                
                GameManager.Instance.unlockedDifficultyList[1] = true;
                SteamAchievementManager.instance.UnlockAchievement("ACHIEVEMENT_DIFF0");

                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene(1);
                }
            }
            else if (GameManager.Instance.GetDifficulty() == 1)
            {
                difficultyCompletePanel.SetActive(true);
                diffAnimPanel1.SetActive(true);

                GameManager.Instance.unlockedDifficultyList[2] = true;
                SteamAchievementManager.instance.UnlockAchievement("ACHIEVEMENT_DIFF1");

                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene(1);
                }
            }
            else if (GameManager.Instance.GetDifficulty() == 2)
            {
                difficultyCompletePanel.SetActive(true);
                diffAnimPanel2.SetActive(true);

                SteamAchievementManager.instance.UnlockAchievement("ACHIEVEMENT_DIFF2");

                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}
