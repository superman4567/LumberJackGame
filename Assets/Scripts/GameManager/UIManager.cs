using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Build panel")]
    [SerializeField] private GameObject strucutres_panel;

    [Header("Build panel Blueprints")]

    [Header("Storm text")]
    [SerializeField] private TMP_Text stormTimerText;
    
    private EnvironmentManager environmentManager;
    private GameObject lastActivePanel; // Store the last active panel here
    private bool strucutresPanelActive = false;
    private bool playerIsInside = false;

    public Action woodCheat;
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        {
            environmentManager = FindObjectOfType<EnvironmentManager>();
            if (environmentManager == null)
            {
                playerIsInside = true;
            }
        }
    }

    private void Start()
    {
        strucutres_panel.SetActive(false);
        lastActivePanel = null; // Initialize lastActivePanel to null
    }

    private void Update()
    {
        closeLastPanel(); // Check for closing the last active panel

        if (playerIsInside) { return; }
        DisplayStormTimer();
    }

    private void closeLastPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && lastActivePanel != null)
        {
            lastActivePanel.SetActive(false);
            lastActivePanel = null; // Reset the last active panel
        }
    }

    public void ActivationStrucutresPanel()
    {
        if (!strucutresPanelActive)
        {
            lastActivePanel = strucutres_panel; // Update the last active panel
            ActivationStrucutresPanel(true);
        }
        else if (strucutresPanelActive)
        {
            ActivationStrucutresPanel(false);
        }
    }

    public void ActivationStrucutresPanel(bool setStructurePanel)
    {
        strucutres_panel.SetActive(setStructurePanel);
        strucutresPanelActive = setStructurePanel;
    }

    public void WoodCheat()
    {
        woodCheat?.Invoke();
    }

    private void DisplayStormTimer()
    {
        float timeText = MathF.Ceiling(environmentManager.timeRemaining);
        stormTimerText.text = timeText.ToString();
    }
}
