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

    [Header("Chest spawner")]
    [SerializeField] private ChestSpawner chestSpawner;
    [SerializeField] private BreakableObjectSpawner breakableSpawner;

    public int currentRound = 0;
    private int orcsToSpawnInCurrentRound;
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
        chestSpawner.SpawnChest();
        breakableSpawner.SpawnBreakables();

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
        }
        if (currentRound == roundToCompleteLevel)
        {
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

        chestSpawner.SpawnChest();
        breakableSpawner.SpawnBreakables();
        currentRound++;

        isStartingNewRound = false; 
    }

    private void Statemachine()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                orcSpawnIncreasePercentage = 0.6f; 
                orcsToSpawnInCurrentRound = 10; 
                roundToCompleteLevel = 25;
                break;

            case 1:
                orcSpawnIncreasePercentage = 1.2f;
                orcsToSpawnInCurrentRound = 25;
                roundToCompleteLevel = 50;
                break;

            case 2:
                orcSpawnIncreasePercentage = 2.5f;
                orcsToSpawnInCurrentRound = 30;
                roundToCompleteLevel = 100;
                break;

            default:
                break;
        }
    }
    public int OrcsSpawnedThisRound()
    {
        return orcsToSpawnInCurrentRound;
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
        Invoke("ResetRoundComplete", 3f);
    }

    private void ResetRoundComplete()
    {
        currentRoundNumber.text = nextRoundNumber.text;
        nextRoundAnimator.SetBool("NextRound", false);
    }

    private void IsDifficultyComplete()
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
