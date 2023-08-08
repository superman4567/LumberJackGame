using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("HUD Buttons")]
    [SerializeField] private GameObject strucutres_panel;
    public Action woodCheat;
    private bool strucutresPanelActive = false;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        strucutres_panel.SetActive(false);
    }

    public void ActivationStrucutresPanel()
    {
        if (!strucutresPanelActive)
        {
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
        woodCheat.Invoke();
    }

}
