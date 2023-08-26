using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Animator aniamtor;

    private DialogueContainer currentDialogue;
    public int currentTextIndex = 0;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShowNextDialogue();
        }
    }

    // Call from anywhere you want to show a tutorial
    private void ShowUI()
    {
        aniamtor.SetTrigger("StartReading");
        dialogueText.text = currentDialogue.description[currentTextIndex];
    }

    public void SetDialogue(DialogueContainer dialogueContainer)
    {
        currentDialogue = dialogueContainer;
        ShowUI();
    }

    public void ShowNextDialogue()
    {
        currentTextIndex++;

        if (currentTextIndex == currentDialogue.description.Length)
        {
            HideTutorialUI();
            return;
        }

        dialogueText.text = currentDialogue.description[currentTextIndex];
    }

    public void HideTutorialUI()
    {
        aniamtor.SetTrigger("FinishedReading");
        currentTextIndex = 0; 
    }
}
