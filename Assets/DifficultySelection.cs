using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    [SerializeField] GameObject difficultyPanel;
    public bool hasPlayerInteractedWithBoard = false;

    private void Start()
    {
        difficultyPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayerInteractedWithBoard) { return; }

        difficultyPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        difficultyPanel.SetActive(false);
    }
}
