using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public static EventHandler<string> OnFinishedDialogue;

    [SerializeField] private DialogueContainer dialogue;

    private PlayerStats player;
    private DialogueManager dialogueManager;

    private int currentDialogueIndex = 0;
    private bool completedDialogue = false;
    private bool isInteractingWithPlayer = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        dialogueManager = DialogueManager.Instance;
    }

    private void Update()
    {
        if (!isInteractingWithPlayer) return;

        // Replace with input system
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!TryUpdateNextDialogue())
            {
                CompleteDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (completedDialogue) return;
        if (other.tag != player.tag) return;
        isInteractingWithPlayer = true;

        dialogueManager.UpdateDialogueText(dialogue.description[currentDialogueIndex]);
        dialogueManager.ShowUI();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != player.tag) return;
        isInteractingWithPlayer = false;

        dialogueManager.HideUI();

        if (completedDialogue)
        {
            Destroy(gameObject);
        }
    }

    private bool TryUpdateNextDialogue()
    {
        if (currentDialogueIndex < dialogue.description.Length - 1)
        {
            currentDialogueIndex++;
            dialogueManager.UpdateDialogueText(dialogue.description[currentDialogueIndex]);
            return true;
        }

        return false;
    }

    private void CompleteDialogue()
    {
        if (currentDialogueIndex >= dialogue.description.Length - 1)
        {
            OnFinishedDialogue?.Invoke(this, tag);
            completedDialogue = true;
            dialogueManager.HideUI();
        }
    }
}
