using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Resources")]
    public int rawWood = 0;
    public int sticks = 0;
    public int planks = 0;
    public int woodenSpikes = 0;
    public int door = 0;

    [Header("Resources UI Elements")]
    public TextMeshProUGUI rawWoodUI;
    public TextMeshProUGUI sticksUI;
    public TextMeshProUGUI planksUI;
    public TextMeshProUGUI woodenSpikesUI;
    public TextMeshProUGUI doorUI;

    public static GameManager Instance;

    private void Start()
    {
        Instance = this;

        rawWood = PlayerPrefs.GetInt("RawWood");
        sticks = PlayerPrefs.GetInt("Sticks", sticks);
        planks = PlayerPrefs.GetInt("Planks", planks);
        woodenSpikes = PlayerPrefs.GetInt("WoodenSpikes", woodenSpikes);
        door = PlayerPrefs.GetInt("Door", door);

        UpdateUI();
    }

    public void SetSaveData()
    {
        PlayerPrefs.SetInt("RawWood", rawWood);
        PlayerPrefs.SetInt("Sticks", sticks);
        PlayerPrefs.SetInt("Planks", planks);
        PlayerPrefs.SetInt("WoodenSpikes", woodenSpikes);
        PlayerPrefs.SetInt("Door", door);
    }

    private void UpdateUI()
    {
        rawWoodUI.text = "Wood: " + rawWood.ToString();
        sticksUI.text = "Sticks: " + sticks.ToString();
        planksUI.text = "Planks: " + planks.ToString();
        woodenSpikesUI.text = "WoodenSpikes: " + woodenSpikes.ToString();
        doorUI.text = "Doors: " + door.ToString();
    }

    public void AddWood()
    {
        rawWood += 2;
        UpdateUI();
        SetSaveData();
        Debug.Log($"raw wood amount= {rawWood}");
    }

    public void AddSticks()
    {
        sticks += 2;
        UpdateUI();
        SetSaveData();
        Debug.Log($"sticks amount= {sticks}");
    }

    public void AddPlanks()
    {
        planks += 2;
        UpdateUI();
        SetSaveData();
        Debug.Log($"planks amount= {planks}");
    }

    public void SubstractWood(int woodToSubsubstract)
    {
        rawWood -= woodToSubsubstract;
        UpdateUI();
        SetSaveData();
    }
}
