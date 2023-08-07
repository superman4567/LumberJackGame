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
            strucutres_panel.SetActive(true);
            strucutresPanelActive = true;

        }
        else if (strucutresPanelActive)
        {
            strucutres_panel.SetActive(false);
            strucutresPanelActive = false;
        }
    }

    public void WoodCheat()
    {
        woodCheat.Invoke();
    }

}
