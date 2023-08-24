using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] public List<GameObject> panels = new List<GameObject>();

    [Header("Storm text")]
    private EnvironmentManager environmentManager;
    private bool playerIsInside = false;

    public Action woodCheat;
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You have another instance of the UI manager");
            Destroy(this);
        }
        Instance = this;
        environmentManager = FindObjectOfType<EnvironmentManager>();
        if (environmentManager == null)
        {
            playerIsInside = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllPanels();
        }

        if (playerIsInside) { return; }
    }

    public void OpenPanelByIndex(int panelIndex)
    {
        CloseAllPanels();
        panels[panelIndex].SetActive(true);
    }

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
