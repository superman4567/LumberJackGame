using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public static GameManager Instance;

    [Header("Resources")]
    public int wood = 0;
    public int coins = 0;
    

    [Header("Resources UI Elements")]
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI coinsUI;
 

    public enum ResourceType
    {
        Wood,
        Coins,
    }

    private void Start()
    {
        Instance = this;
        UpdateUI();
    }

    public void LoadData(GameData data)
    {
        this.wood = data.wood;
        this.coins = data.sticks;
    }

    public void SaveData(ref GameData data)
    {
        data.wood = this.wood;
        data.sticks = this.coins;
    }

    private void UpdateUI()
    {
        woodUI.text = "Wood: " + wood.ToString();
        coinsUI.text = "Sticks: " + coins.ToString();
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                wood += amount;
                break;
            case ResourceType.Coins:
                coins += amount;
                break;
           
            default: Debug.LogError("Add resource method in game manager not working properly");
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
}
