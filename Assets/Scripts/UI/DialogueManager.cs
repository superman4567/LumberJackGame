using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Animator aniamtor;
    public Action OnNextDialogue;

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
            if(currentTextIndex > currentDialogue.description.Length) { return; }
            ShowNextDialogue();
        }
    }

    private void ShowUI()
    {
        aniamtor.SetBool("IsReading", true);
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
        OnNextDialogue?.Invoke();

        if (currentTextIndex == currentDialogue.description.Length)
        {
            HideTutorialUI();
            ResetValues();
            return;
        }

        if (currentTextIndex > currentDialogue.description.Length) { return; }
        dialogueText.text = currentDialogue.description[currentTextIndex];
    }

    public void HideTutorialUI()
    {
        aniamtor.SetBool("IsReading", false);
        
    }

    public void ResetValues()
    {
        currentTextIndex = 0;
    }
}
