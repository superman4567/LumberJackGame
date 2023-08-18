using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public static GameManager Instance;

    [Header("Resources")]
    public int wood = 0;
    public int sticks = 0;
    public int planks = 0;
    public int woodenSpikes = 0;
    public int rock = 0;

    [Header("Resources UI Elements")]
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI sticksUI;
    public TextMeshProUGUI planksUI;
    public TextMeshProUGUI woodenSpikesUI;
    public TextMeshProUGUI rockUI;

    public enum ResourceType
    {
        Wood,
        Sticks,
        Planks,
        WoodenSpikes,
        Rock
    }

    private void Start()
    {
        Instance = this;
        UpdateUI();
    }

    public void LoadData(GameData data)
    {
        this.wood = data.wood;
        this.sticks = data.sticks;
        this.planks = data.planks;
        this.woodenSpikes = data.woodenSpikes;
        this.rock = data.rock;
    }

    public void SaveData(ref GameData data)
    {
        data.wood = this.wood;
        data.sticks = this.sticks;
        data.planks = this.planks;
        data.woodenSpikes = this.woodenSpikes;
        data.rock = this.rock;
    }

    private void UpdateUI()
    {
        woodUI.text = "Wood: " + wood.ToString();
        sticksUI.text = "Sticks: " + sticks.ToString();
        planksUI.text = "Planks: " + planks.ToString();
        woodenSpikesUI.text = "WoodenSpikes: " + woodenSpikes.ToString();
        rockUI.text = "Rock: " + rock.ToString();
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                wood += amount;
                break;
            case ResourceType.Sticks:
                sticks += amount;
                break;
            case ResourceType.Planks:
                planks += amount;
                break;
            case ResourceType.WoodenSpikes:
                woodenSpikes += amount;
                break;
            case ResourceType.Rock:
                rock += amount;
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
            case ResourceType.Sticks:
                sticks += amount;
                break;
            case ResourceType.Planks:
                planks += amount;
                break;
        }

        UpdateUI();
    }
}
