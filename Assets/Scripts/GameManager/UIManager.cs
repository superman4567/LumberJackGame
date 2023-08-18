using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private List<GameObject> panels;

    [Header("Storm text")]
    [SerializeField] private TMP_Text stormTimerText;
    private EnvironmentManager environmentManager;
    private bool playerIsInside = false;

    public Action woodCheat;
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        environmentManager = FindObjectOfType<EnvironmentManager>();
        if (environmentManager == null)
        {
            playerIsInside = true;
        }
    }

    private void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllPanels();
        }

        if (playerIsInside) { return; }
        DisplayStormTimer();
    }

    private void DisplayStormTimer()
    {
        float timeText = Mathf.Ceil(environmentManager.timeRemaining); // Changed MathF to Mathf
        stormTimerText.text = timeText.ToString();
    }

    public void OpenPanelByIndex(int panelIndex)
    {
        Debug.Log(this == Instance);
        Debug.Log(panels.Count);
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == panelIndex)
            {
                Debug.Log(panels[i]);
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
