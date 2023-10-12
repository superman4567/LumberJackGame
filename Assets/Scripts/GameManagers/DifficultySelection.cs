using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    [SerializeField] GameObject difficultyPanel;

    private bool hasInteractedWithBoard = false;

    private void Start()
    {
        difficultyPanel.SetActive(false);

        ShowDialogue.OnFinishedDialogue += ShowDialogue_OnFinishedDialogue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteractedWithBoard) { return; }

        difficultyPanel.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasInteractedWithBoard) { return; }

        difficultyPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        difficultyPanel.SetActive(false);
    }

    private void ShowDialogue_OnFinishedDialogue(object sender, string e)
    {
        if (e == "Board")
        {
            hasInteractedWithBoard = true;

            ShowDialogue.OnFinishedDialogue -= ShowDialogue_OnFinishedDialogue;
        }
    }
}
