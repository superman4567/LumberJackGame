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
    public TextMeshProUGUI RockUI;

    private void OnEnable()
    {
        UIManager.Instance.woodCheat += AddWoodCheat;
    }

    private void OnDisable()
    {
        UIManager.Instance.woodCheat -= AddWoodCheat;
    }

    private void Start()
    {
        Instance = this;
        UpdateUI();
    }

    public void LoadData(GameData data)
    {
        this.wood = data.wood;
    }

    public void SaveData(ref GameData data)
    {
        data.wood = this.wood;
    }

    private void UpdateUI()
    {
        woodUI.text = "Wood: " + wood.ToString();
        sticksUI.text = "Sticks: " + sticks.ToString();
        planksUI.text = "Planks: " + planks.ToString();
        woodenSpikesUI.text = "WoodenSpikes: " + woodenSpikes.ToString();
        RockUI.text = "Rock: " + rock.ToString();
    }

    public void AddWood()
    {
        wood += 2;
        UpdateUI();
    }

    public void AddSticks()
    {
        sticks += 2;
        UpdateUI();
    }

    public void AddPlanks()
    {
        planks += 2;
        UpdateUI();
    }

    public void SubstractWood(int woodToSubsubstract)
    {
        wood -= woodToSubsubstract;
        UpdateUI();
    }

    public void AddWoodCheat()
    {
        if(UIManager.Instance.woodCheat != null)
        {
            wood += 200;
            UpdateUI();
        }
    }

    
}
