using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public static GameManager Instance;

    [Header("Difficulty")]
    public int selectedDifficulty;
    public List<bool> unlockedDifficultyList = new List<bool>();

    [Header("Resources")]
    public int wood = 0;
    public int coins = 0;
    public int woodMultiplier = 0;
    public int coinMultiplier = 0;

    [Header("Resources UI Elements")]
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI coinsUI;

    [Header("Buttons")]
    public Button[] difficultyButtons;
    [SerializeField] private Sprite selected;

    [Header("Buttons")]
    [SerializeField] private GameObject pausePanel;

    public enum ResourceType
    {
        Wood,
        Coins,
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        unlockedDifficultyList.Add(true); 
        for (int i = 1; i < 3; i++) 
        {
            unlockedDifficultyList.Add(false); 
        }
    }

    private void Start()
    {
        UpdateUI();
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        ClickOnUI();
        PauseGame();
    }

    private void ClickOnUI()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1)) { return; }
        ButtonStates();

        if (Input.GetMouseButtonDown(0))
        {
            SelectButton();
        }
    }

    public void LoadData(GameData data)
    {
        this.wood = data.wood;
        this.coins = data.coins;
        this.selectedDifficulty = data.difficulty;
    }

    public void SaveData(ref GameData data)
    {
        data.wood = this.wood;
        data.coins = this.coins;
        data.difficulty = this.selectedDifficulty;
    }

    private void UpdateUI()
    {
        woodUI.text = wood.ToString();
        coinsUI.text = coins.ToString();
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                wood += (amount * woodMultiplier);
                break;
            case ResourceType.Coins:
                coins += (amount * coinMultiplier);
                break;

            default:
                Debug.LogError("Add resource method in game manager not working properly");
                break;
        }

        UpdateUI();
    }

    public void SubstractResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                wood += amount;
                break;
            case ResourceType.Coins:
                coins += amount;
                break;

        }

        UpdateUI();
    }

    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.05f;
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void GoToCabin()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonStates()
    {
        difficultyButtons[0].interactable = true;

        if (unlockedDifficultyList[1] == true)
        {
            difficultyButtons[1].interactable = true;
        }
        else
        {
            difficultyButtons[1].interactable = false;
        }

        if (unlockedDifficultyList[2] == true)
        {
            difficultyButtons[2].interactable = true;
        }
        else
        {
            difficultyButtons[2].interactable = false;
        }

        IsAButtonSelected();
    }

    private bool IsAButtonSelected()
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            Image currentSprite = difficultyButtons[i].image;
            Debug.Log(currentSprite.sprite);

            if (currentSprite.sprite == selected)
            {
                Debug.Log("AA");
                return true;
            }
        }
        
        return false;
    }

    private void SelectButton()
    {
        difficultyButtons[selectedDifficulty].Select();
    }

    public void SetDifficulty(int difficulty)
    {
        selectedDifficulty = difficulty;
        SelectButton();
    }

    public int GetDifficulty()
    {
        return selectedDifficulty;
    }
}
