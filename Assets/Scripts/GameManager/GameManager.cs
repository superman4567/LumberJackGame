using Cinemachine;
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
    private int wood;
    private int coins;
    private int woodMultiplier = 1;
    private int coinMultiplier = 1;

    [Header("Resources UI Elements")]
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI coinsUI;

    [Header("Buttons")]
    public Button[] difficultyButtons;
    [SerializeField] private Sprite selected;

    [Header("PausePanel")]
    [SerializeField] private GameObject pausePanel;

    [Header("PowerUp variables")]
    public float chestHealthGain = 25f;

    [Header("StorePanel")]
    [SerializeField] private GameObject storePanel;
    [SerializeField] private CinemachineVirtualCamera characterCamera;
    [SerializeField] private CinemachineVirtualCamera storeCamera;
    [SerializeField] private TextMeshProUGUI coinAmount;

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
        wood = 1000;
        UpdateUI();
        pausePanel.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex != 1) { return; }
        storePanel.SetActive(false);

    }

    private void Update()
    {
        ClickOnUI();
        PauseGame();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            coinAmount.text = coins.ToString();
        }
    }

    public void LoadData(GameData data)
    {
        this.coins = data.coins;
        this.selectedDifficulty = data.difficulty;
        this.chestHealthGain = data.chestHealthGain;
    }

    public void SaveData(GameData data)
    {
        data.coins = this.coins;
        data.difficulty = this.selectedDifficulty;
        data.chestHealthGain = this.chestHealthGain;
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
                wood -= amount;
                break;
            case ResourceType.Coins:
                coins -= amount;
                break;
        }

        UpdateUI();
    }

    public void IncreaseWoodMultiplier(int amount)
    {
        woodMultiplier = amount;
    }

    public void IncreaseCoindMultiplier(int amount)
    {
        coinMultiplier = amount;
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
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        DataPersistenceManager.instance.SaveGame();
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

            if (currentSprite.sprite == selected)
            {
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

    public void CloseCurrentUI(GameObject ui)
    {
        ui.SetActive(false);    
    }

    public void OpenStore()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) { return; }
        Invoke("OpenStorePanel", 1.2f);
        storeCamera.Priority = 2;
    }

    private void OpenStorePanel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) { return; }
        storePanel.SetActive(true);
    }

    public void CloseStore()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) { return; }
        storePanel.SetActive(false);
        storeCamera.Priority = 0;
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetWood()
    {
        return wood;
    }
}
