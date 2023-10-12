using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private Animator difficultyCompleteAnimator;
    [SerializeField] private TextMeshProUGUI completedDifficultyText;
    [SerializeField] private Image victoryImage;
    [SerializeField] private Image defeatImage;

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI kills;
    [SerializeField] private TextMeshProUGUI chestsOpenend;
    [SerializeField] private TextMeshProUGUI treesChopped;

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
    bool backHome = false;

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
        victoryImage.enabled= false;
        defeatImage.enabled= false;
    }

    private void Update()
    {
        Cheatcodes();

        if (currentRound == roundToCompleteLevel)
        {
            IsDifficultyComplete();
        }
        else if (!isStartingNewRound && orcsKilledInCurrentRound == orcsToSpawnInCurrentRound)
        {
            StartCoroutine(StartNewRoundCoroutine());
        }
        if (Input.anyKeyDown && backHome)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void Cheatcodes()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            currentRound = 25;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            currentRound = 50;
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            currentRound = 100;
        }
    }

    private IEnumerator StartNewRoundCoroutine()
    {
        isStartingNewRound = true; // Set the flag to prevent multiple calls
        RoundCompplete(currentRound);

        yield return new WaitForSeconds(5.0f);

        orcsToSpawnInCurrentRound = Mathf.CeilToInt(orcsToSpawnInCurrentRound + orcSpawnIncreasePercentage);
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
                orcSpawnIncreasePercentage = 2f; 
                orcsToSpawnInCurrentRound = 10; 
                roundToCompleteLevel = 25;
                break;

            case 1:
                orcSpawnIncreasePercentage = 2.25f;
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
            ChangeDiffCompleteText(true);
            GameManager.Instance.diffiuclty1Unlocked = true;
            SteamAchievementManager.instance.UnlockAchievement("ACHIEVEMENT_DIFF0");
            Invoke("BackToMainMenu", 2f);
        }
        else if (GameManager.Instance.GetDifficulty() == 1)
        {
            ChangeDiffCompleteText(true);
            GameManager.Instance.diffiuclty2Unlocked = true;
            SteamAchievementManager.instance.UnlockAchievement("ACHIEVEMENT_DIFF1");
            Invoke("BackToMainMenu", 2f);
        }
        else if (GameManager.Instance.GetDifficulty() == 2)
        {
            ChangeDiffCompleteText(true);
            SteamAchievementManager.instance.UnlockAchievement("ACHIEVEMENT_DIFF2");
            Invoke("BackToMainMenu", 2f);
        }
    }

    public void ChangeDiffCompleteText(bool victory)
    {
        difficultyCompletePanel.SetActive(true);
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                completedDifficultyText.text = "EASY DIFFICULTY";
                break;

            case 1:
                completedDifficultyText.text = "MEDIUM DIFFICULTY";
                break;

            case 2:
                completedDifficultyText.text = "HARD DIFFICULTY";
                break;

            default:
                completedDifficultyText.text = "I don't know";
                break;
        }

        if (victory)
        {
            victoryImage.enabled = true;
        }
        else
        {
            defeatImage.enabled = true;
        }

        GameManager.Instance.roundTimer = false;
        time.text = Mathf.Round(GameManager.Instance.thisRunTimer).ToString();
        kills.text = GameManager.Instance.thisRunOrcsSlayed.ToString();
        chestsOpenend.text = GameManager.Instance.thisRunChestsOpened.ToString();
        treesChopped.text = GameManager.Instance.thisRunChoppedTrees.ToString();
    }

    private void BackToMainMenu()
    {
        backHome = true;
    }
}
